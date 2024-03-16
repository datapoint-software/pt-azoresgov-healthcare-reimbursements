import { Injectable } from "@angular/core";
import { Manager } from "../../feature.abstractions";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { FormGroup, FormControl } from "@angular/forms";
import { Store } from "@ngrx/store";
import { configurationRowVersionId, legalRepresentativeRowVersionId, legalRepresentativeWritting, state } from "../rx/process-capture.selectors";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { setControlsEnabled } from "../../../helpers/reactive-forms.helper";
import { Actions, ofType } from "@ngrx/effects";
import { configure, deleteLegalRepresentative, writeLegalRepresentative } from "../rx/process-capture.actions";
import { filter, map, takeUntil } from "rxjs";
import { Validators } from "../../../app.validators";

@Injectable()
export class ProcessCaptureLegalRepresentativeManager extends Manager<ProcessCaptureState> {

  public readonly written$ = this.of(legalRepresentativeRowVersionId)
    .pipe(map((rowVersionId) => !!rowVersionId));

  public readonly writting$ = this.of(legalRepresentativeWritting)

  public readonly form = new FormGroup({
    enabled: new FormControl(false, []),
    name: new FormControl('', [ Validators.required, Validators.maxLength(128) ]),
    taxNumber: new FormControl('', [ Validators.required, Validators.taxNumber ]),
    emailAddress: new FormControl('', [ Validators.email, Validators.maxLength(256) ]),
    faxNumber: new FormControl('', [ Validators.maxLength(16), Validators.phoneNumber ]),
    mobileNumber: new FormControl('', [ Validators.maxLength(16), Validators.phoneNumber ]),
    phoneNumber: new FormControl('', [ Validators.maxLength(16), Validators.phoneNumber ])
  });

  constructor(store: Store, private readonly actions$: Actions) {
    super(store, state);
  }

  private configure({ legalRepresentative: patientLegalRepresentative }: ProcessCaptureState) {

    if (!patientLegalRepresentative) {
      this.disable();
      return;
    }

    this.form.reset({
      enabled: true,
      name: patientLegalRepresentative.name || null,
      taxNumber: patientLegalRepresentative.taxNumber || null,
      emailAddress: patientLegalRepresentative.emailAddress || null,
      faxNumber: patientLegalRepresentative.faxNumber || null,
      mobileNumber: patientLegalRepresentative.mobileNumber || null,
      phoneNumber: patientLegalRepresentative.phoneNumber || null
    }, {
      emitEvent: false
    });
  }

  private disable() {

    this.form.reset({
      enabled: false
    }, {
      emitEvent: false
    });

    const fc = this.form.controls;

    setControlsEnabled(false, [
      fc.name,
      fc.taxNumber,
      fc.emailAddress,
      fc.faxNumber,
      fc.mobileNumber,
      fc.phoneNumber
    ]);
  }

  private enabledChanges(enabled: boolean) {

    const fc = this.form.controls;

    setControlsEnabled(enabled, [
      fc.name,
      fc.taxNumber,
      fc.emailAddress,
      fc.faxNumber,
      fc.mobileNumber,
      fc.phoneNumber
    ]);

    if (!enabled) {

      this.dispatch(deleteLegalRepresentative());

      this.form.reset({
        enabled: false
      }, {
        emitEvent: false
      });
    }
  }

  private formChanges(legalRepresentative: Partial<{
    enabled: boolean | null;
    name: string | null;
    taxNumber: string | null;
    emailAddress: string | null;
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
  }>): void {

    if (!this.form.valid)
      return;

    if (!this.form.value.enabled)
      return;

    this.write(true, this.form.value);
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => this.configure(payload));

    this.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((legalRepresentative) => this.formChanges(legalRepresentative));

    this.form.controls.enabled.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((enabled) => this.enabledChanges(enabled!));
  }

  public submit() {

    if (!this.form.valid)
      return;

    if (!this.form.value.enabled)
      return;


  }

  private write(debounce: boolean, legalRepresentative: Partial<{ name: string | null; taxNumber: string | null; emailAddress: string | null; faxNumber: string | null; mobileNumber: string | null; phoneNumber: string | null; }>) {
    this.dispatch(writeLegalRepresentative({
      payload: {
        debounce,
        legalRepresentative: {
          name: legalRepresentative.name!,
          taxNumber: legalRepresentative.taxNumber!,
          emailAddress: legalRepresentative.emailAddress || undefined,
          faxNumber: legalRepresentative.faxNumber || undefined,
          mobileNumber: legalRepresentative.mobileNumber || undefined,
          phoneNumber: legalRepresentative.phoneNumber || undefined,
        }
      }
    }));
  }
}
