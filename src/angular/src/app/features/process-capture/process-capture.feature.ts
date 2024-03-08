import { Actions, concatLatestFrom, ofType, provideEffects } from "@ngrx/effects";
import { configure, debounceWritting, dispose, init, writeConfiguration, writePatient } from "./rx/process-capture.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-capture.constants";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { patient, patientHealthNumber, patientName, patientTaxNumber, process, processNumber, state, writting } from "./rx/process-capture.selectors";
import { ProcessCaptureEffects } from "./rx/process-capture.effects";
import { ProcessCaptureState } from "./rx/process-capture.state";
import { reducer } from "./rx/process-capture.reducer";
import { Store, provideState } from "@ngrx/store";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Subject, debounceTime, distinct, filter, skip, takeUntil, tap } from "rxjs";
import { APP_AUTOSAVE_DELAY_MS } from "../../app.constants";

@Injectable()
export class ProcessCaptureFeature extends Feature<ProcessCaptureState> {

  private dispose$?: Subject<boolean>;

  readonly configuration = new FormGroup({
    machadoJosephEnabled:new FormControl(false, []),
    documentIssueDateBypassEnabled:new FormControl(false, []),
    reimbursementLimitBypassEnabled:new FormControl(false, [])
  });

  readonly patient = new FormGroup({
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

  readonly legalRepresentative = new FormGroup({

  });

  readonly patient$ = this.of(patient);

  readonly patientHealthNumber$ = this.of(patientHealthNumber);

  readonly patientName$ = this.of(patientName);

  readonly patientTaxNumber$ = this.of(patientTaxNumber);

  readonly process$ = this.of(process);

  readonly processNumber$ = this.of(processNumber);

  readonly writting$ = this.of(writting);

  constructor(store: Store, private readonly actions$: Actions) {

    super(store, state, dispose, (r) => init({
      payload: {
        processId: r.paramMap.get('processId')!
      }
    }));
  }

  public writeConfiguration(configuration: {
    machadoJosephEnabled: boolean;
    documentIssueDateBypassEnabled: boolean;
    reimbursementLimitBypassEnabled: boolean;
  }) {
    return this.dispatch(writeConfiguration({
      payload: { ...configuration }
    }));
  }

  public writePatient(patient: {
    addressLine1: string;
    addressLine2?: string;
    addressLine3?: string;
    postalCode: string;
    postalCodeArea: string;
    emailAddress?: string;
    faxNumber?: string;
    mobileNumber?: string;
    phoneNumber?: string;
  }) {
    return this.dispatch(writePatient({
      payload: { ...patient }
    }));
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    this.dispose$ = new Subject<boolean>();

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => {

        this.configuration.setValue({
          machadoJosephEnabled: payload.configuration?.machadoJosephEnabled || false,
          documentIssueDateBypassEnabled: payload.configuration?.documentIssueDateBypassEnabled || false,
          reimbursementLimitBypassEnabled: payload.configuration?.reimbursementLimitBypassEnabled || false
        });

        this.patient.setValue({
          addressLine1: payload.patient.addressLine1 || null,
          addressLine2: payload.patient.addressLine2 || null,
          addressLine3: payload.patient.addressLine3 || null,
          postalCode: payload.patient.postalCode || null,
          postalCodeArea: payload.patient.postalCodeArea || null,
          emailAddress: payload.patient.emailAddress || null,
          faxNumber: payload.patient.faxNumber || null,
          mobileNumber: payload.patient.mobileNumber || null,
          phoneNumber: payload.patient.phoneNumber || null
        });

        this.configuration.updateValueAndValidity();
        this.patient.updateValueAndValidity();
      });

    this.configuration.valueChanges
      .pipe(skip(2))
      .pipe(takeUntil(this.dispose$))
      .pipe(filter(() => this.configuration.valid))
      .pipe(distinct())
      .pipe(tap(() => this.dispatch(debounceWritting())))
      .pipe(debounceTime(APP_AUTOSAVE_DELAY_MS))
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => {
        this.writeConfiguration({
          machadoJosephEnabled: this.configuration.value.machadoJosephEnabled || false,
          documentIssueDateBypassEnabled: this.configuration.value.documentIssueDateBypassEnabled || false,
          reimbursementLimitBypassEnabled: this.configuration.value.reimbursementLimitBypassEnabled || false
        });
      });

    this.patient.valueChanges
      .pipe(skip(2))
      .pipe(takeUntil(this.dispose$))
      .pipe(filter(() => this.patient.valid))
      .pipe(distinct())
      .pipe(tap(() => this.dispatch(debounceWritting())))
      .pipe(debounceTime(APP_AUTOSAVE_DELAY_MS))
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => {
        this.writePatient({
          addressLine1: this.patient.value.addressLine1!,
          addressLine2: this.patient.value.addressLine2 || undefined,
          addressLine3: this.patient.value.addressLine3 || undefined,
          postalCode: this.patient.value.postalCode!,
          postalCodeArea: this.patient.value.postalCodeArea!,
          emailAddress: this.patient.value.emailAddress || undefined,
          faxNumber: this.patient.value.faxNumber || undefined,
          mobileNumber: this.patient.value.mobileNumber || undefined,
          phoneNumber: this.patient.value.phoneNumber || undefined,
        });
      });

    await super.init(activatedRoute, router);
  }

  public override async dispose(): Promise<void> {

    if (this.dispose$) {

      this.dispose$.next(true);
      this.dispose$.complete();

      delete this.dispose$;
    }

    await super.dispose();
  }
}

export const provideProcessCaptureFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessCaptureFeature
  ]),

  provideEffects(ProcessCaptureEffects),
  provideState(FEATURE_NAME, reducer)
];

