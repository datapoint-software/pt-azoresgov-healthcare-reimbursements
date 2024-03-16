import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { ActivatedRouteSnapshot } from "@angular/router";
import { provideEffects } from "@ngrx/effects";
import { Store, provideState } from "@ngrx/store";
import { Feature } from "../feature.abstractions";
import { ProcessCaptureConfigurationManager } from "./managers/process-capture-configuration.manager";
import { ProcessCaptureFamilyIncomeStatementManager } from "./managers/process-capture-family-income-statement.manager";
import { ProcessCaptureLegalRepresentativeManager } from "./managers/process-capture-legal-representative.manager";
import { ProcessCapturePatientManager } from "./managers/process-capture-patient.manager";
import { ProcessCapturePaymentManager } from "./managers/process-capture-payment.manager";
import { ProcessCaptureUnemploymentManager } from "./managers/process-capture-unemployment.manager";
import { FEATURE_NAME } from "./process-capture.constants";
import { dispose, init } from "./rx/process-capture.actions";
import { ProcessCaptureEffects } from "./rx/process-capture.effects";
import { reducer } from "./rx/process-capture.reducer";
import { processId, processNumber, state, writting } from "./rx/process-capture.selectors";
import { ProcessCaptureState } from "./rx/process-capture.state";

@Injectable()
export class ProcessCaptureFeature extends Feature<ProcessCaptureState> {

  public readonly id$ = this.of(processId);

  public readonly number$ = this.of(processNumber);

  public readonly writting$ = this.of(writting);

  constructor(
    store: Store,
    readonly configuration: ProcessCaptureConfigurationManager,
    readonly familyIncomeStatement: ProcessCaptureFamilyIncomeStatementManager,
    readonly legalRepresentative: ProcessCaptureLegalRepresentativeManager,
    readonly patient: ProcessCapturePatientManager,
    readonly payment: ProcessCapturePaymentManager,
    readonly unemployment: ProcessCaptureUnemploymentManager
  ) {
    super(store, state, [
      configuration,
      familyIncomeStatement,
      legalRepresentative,
      patient,
      payment,
      unemployment
    ]);
  }

  protected override dispose$$$() {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot) {
    return init({
      payload: {
        processId: activatedRoute.paramMap.get('processId')!
      }
    });
  }
}

export const provideProcessCaptureFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessCaptureFeature,
    ProcessCaptureConfigurationManager,
    ProcessCaptureFamilyIncomeStatementManager,
    ProcessCaptureLegalRepresentativeManager,
    ProcessCapturePatientManager,
    ProcessCapturePaymentManager,
    ProcessCaptureUnemploymentManager
  ]),

  provideEffects(ProcessCaptureEffects),
  provideState(FEATURE_NAME, reducer)
];

