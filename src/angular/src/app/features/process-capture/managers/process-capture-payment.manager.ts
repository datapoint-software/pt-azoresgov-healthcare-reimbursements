import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Manager } from "../../feature.abstractions";
import { bankName, bankSwiftCode, paymentConfigurationRowVersionId, paymentWritting, state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Actions, ofType } from "@ngrx/effects";
import { combineLatest, filter, map, switchMap, take, takeUntil, withLatestFrom } from "rxjs";
import { clearBankResult, configure, searchBank, searchBankComplete, watchPayment, writePayment } from "../rx/process-capture.actions";
import { ProcessCaptureOptionsPaymentResultModel } from "../../../clients/process-capture/process-capture.models";
import { PaymentReceiver } from "../../../enums/payment-receiver.enum";
import { PaymentMethod } from "../../../enums/payment-method.enum";
import { setControlsEnabled } from "../../../helpers/reactive-forms.helper";
import { ProcessCaptureLegalRepresentativeManager } from "./process-capture-legal-representative.manager";
import { Validators } from "../../../app.validators";

@Injectable()
export class ProcessCapturePaymentManager extends Manager<ProcessCaptureState> {

  public readonly bankName$ = this.of(bankName);

  public readonly bankSwiftCode$ = this.of(bankSwiftCode);

  public readonly written$ = this.of(paymentConfigurationRowVersionId)
    .pipe(map((paymentConfigurationRowVersionId) => !!paymentConfigurationRowVersionId));

  public readonly writting$ = this.of(paymentWritting)
    .pipe(map((writting) => !!writting));

  public readonly form = new FormGroup({
    method: new FormControl((null as PaymentMethod | null), [ Validators.required ]),
    receiver: new FormControl((null as PaymentReceiver | null), [ Validators.required ]),
    iban: new FormControl((null as string | null), [ Validators.required, Validators.iban ]),
    swift: new FormControl((null as string | null), [ Validators.required ])
  });

  constructor(
    store: Store,
    private readonly legalRepresentative: ProcessCaptureLegalRepresentativeManager,
    private readonly actions$: Actions) {
    super(store, state);
  }

  private configure(payment: ProcessCaptureOptionsPaymentResultModel | undefined) {

    const method = payment?.method || PaymentMethod.Cash;

    this.form.reset({
      method,
      receiver: payment?.receiver || PaymentReceiver.Patient,
      iban: payment?.iban || null,
      swift: payment?.swift || null
    }, {
      emitEvent: false
    });

    this.methodChanges(method);
  }

  private configureSwiftControl(swiftCode: string) {

    const swiftCodeControl = this.form.controls.swift;

    if (swiftCode === swiftCodeControl.value)
      return;

    swiftCodeControl.setValue(swiftCode, { emitEvent: true });
  }

  private legalRepresentativeEnabledChanges(enabled: boolean) {

    if (this.form.controls.receiver.value === PaymentReceiver.LegalRepresentative && !enabled) {
      this.form.controls.receiver.reset(PaymentReceiver.Patient, { emitEvent: true });
    }
  }

  private ibanChanges(iban: string) {

    if (!this.form.controls.iban.valid) {
      this.form.controls.swift.reset(null, { emitEvent: false });
      this.dispatch(clearBankResult());
      return;
    }

    this.dispatch(searchBank({
      payload: {
        iban
      }
    }));
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => this.configure(payload.payment));

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(searchBankComplete))
      .subscribe(({ payload }) => this.configureSwiftControl(payload.swiftCode));

    this.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((payment) => this.formChanges(payment));

    this.form.controls.iban.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((iban) => this.ibanChanges(iban!));

    this.form.controls.method.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((method) => this.methodChanges(method!));

    this.legalRepresentative.form.controls.enabled.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((enabled) => this.legalRepresentativeEnabledChanges(enabled!));
  }

  private formChanges(payment: Partial<{
    method: PaymentMethod | null;
    receiver: PaymentReceiver | null;
    iban: string | null;
    swift: string | null;
  }>): void {

    if (!this.form.valid)
      return;

    this.write(true, this.form.value);
  }

  private methodChanges(method: PaymentMethod) {

    const enabled = method === PaymentMethod.WireTransfer;

    const { iban, swift } = this.form.controls;

    setControlsEnabled(enabled, [
      iban,
      swift
    ]);

    if (!enabled) {

      for (const c of [ iban, swift ])
        c.reset(null, { emitEvent: false });

      this.dispatch(clearBankResult());
    }
  }

  public watch() {

    if (!this.form.valid)
      return;

    const payment = this.form.value;

    this.dispatch(watchPayment({
      payload: {
        method: payment.method!,
        receiver: payment.receiver!,
        iban: payment.iban || undefined,
        swift: payment.swift || undefined
      }
    }));
  }

  public submit() {

    if (!this.form.valid)
      return;

    this.write(false, this.form.value);
  }

  public write(debounce: boolean, payment: Partial<{
    method: PaymentMethod | null;
    receiver: PaymentReceiver | null;
    iban: string | null;
    swift: string | null;
  }>) {
    this.dispatch(writePayment({
      payload: {
        debounce,
        payment: {
          method: payment.method!,
          receiver: payment.receiver!,
          iban: payment.iban || undefined,
          swift: payment.swift || undefined
        }
      }
    }));
  }
}
