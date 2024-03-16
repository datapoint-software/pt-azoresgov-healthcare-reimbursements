import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Actions, ofType } from "@ngrx/effects";
import { Store } from "@ngrx/store";
import { filter, map, take, takeUntil } from "rxjs";
import { Manager } from "../../feature.abstractions";
import { configure, writePatient } from "../rx/process-capture.actions";
import { patientHealthNumber, patientName, patientRowVersionId, patientTaxNumber, patientWritting, state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";

@Injectable()
export class ProcessCapturePatientManager extends Manager<ProcessCaptureState> {

  public readonly healthNumber$ = this.of(patientHealthNumber);

  public readonly name$ = this.of(patientName);

  public readonly taxNumber$ = this.of(patientTaxNumber);

  public readonly written$ = this.of(patientRowVersionId)
    .pipe(map((rowVersionId) => !!rowVersionId));

  public readonly writting$ = this.of(patientWritting);

  public readonly form = new FormGroup({
    addressLine1: new FormControl('', [ Validators.required, Validators.maxLength(128) ]),
    addressLine2: new FormControl('', [ Validators.maxLength(128) ]),
    addressLine3: new FormControl('', [ Validators.maxLength(128) ]),
    postalCode: new FormControl('', [ Validators.required, Validators.maxLength(16), Validators.pattern(/^\d{4}\-\d{3}$/) ]),
    postalCodeArea: new FormControl('', [ Validators.required, Validators.maxLength(128) ]),
    emailAddress: new FormControl('', [ Validators.email, Validators.maxLength(256) ]),
    faxNumber: new FormControl('', [ Validators.maxLength(16) ]),
    mobileNumber: new FormControl('', [ Validators.maxLength(16) ]),
    phoneNumber: new FormControl('', [ Validators.maxLength(16) ])
  });

  constructor(store: Store, private readonly actions$: Actions) {
    super(store, state);
  }

  private configure(configuration: ProcessCaptureState) {

    this.form.reset({
      addressLine1: configuration.patient.addressLine1,
      addressLine2: configuration.patient.addressLine2 || null,
      addressLine3: configuration.patient.addressLine3 || null,
      postalCode: configuration.patient.postalCode || null,
      postalCodeArea: configuration.patient.postalCodeArea || null,
      emailAddress: configuration.patient.emailAddress || null,
      faxNumber: configuration.patient.faxNumber || null,
      mobileNumber: configuration.patient.mobileNumber || null,
      phoneNumber: configuration.patient.phoneNumber || null
    }, {
      emitEvent: false
    });
  }

  override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => this.configure(payload));

    this.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((patient) => this.formChanges(patient));
  }

  private formChanges(patient: Partial<{
    addressLine1: string | null;
    addressLine2: string | null;
    addressLine3: string | null;
    postalCode: string | null;
    postalCodeArea: string | null;
    emailAddress: string | null;
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
  }>) {

    if (!this.form.valid)
      return;

    this.write(true, patient);
  }

  public seen() {

    if (!this.form.valid)
      return;

    this.of(patientRowVersionId)
      .pipe(take(1))
      .pipe(filter((rowVersionId) => !rowVersionId))
      .subscribe(() => this.write(false, this.form.value));
  }

  public submit() {

    if (!this.form.valid)
      return;

    this.write(false, this.form.value);
  }

  private write(debounce: boolean, patient: Partial<{
    addressLine1: string | null;
    addressLine2: string | null;
    addressLine3: string | null;
    postalCode: string | null;
    postalCodeArea: string | null;
    emailAddress: string | null;
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
  }>) {
    this.dispatch(writePatient({
      payload: {
        debounce,
        patient: {
          addressLine1: this.form.value.addressLine1!,
          addressLine2: this.form.value.addressLine2 || undefined,
          addressLine3: this.form.value.addressLine3 || undefined,
          postalCode: this.form.value.postalCode!,
          postalCodeArea: this.form.value.postalCodeArea!,
          emailAddress: this.form.value.emailAddress || undefined,
          faxNumber: this.form.value.faxNumber || undefined,
          mobileNumber: this.form.value.mobileNumber || undefined,
          phoneNumber: this.form.value.phoneNumber || undefined
        }
      }
    }));
  }
}
