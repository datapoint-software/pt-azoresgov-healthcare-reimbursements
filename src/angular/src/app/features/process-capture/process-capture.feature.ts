import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { provideEffects } from "@ngrx/effects";
import { Store, provideState } from "@ngrx/store";
import { Feature } from "../feature.abstractions";
import { ProcessCaptureConfigurationManager } from "./managers/process-capture-configuration.manager";
import { ProcessCaptureDocumentManager } from "./managers/process-capture-document.manager";
import { ProcessCaptureFamilyIncomeStatementManager } from "./managers/process-capture-family-income-statement.manager";
import { ProcessCaptureLegalRepresentativeManager } from "./managers/process-capture-legal-representative.manager";
import { ProcessCapturePatientManager } from "./managers/process-capture-patient.manager";
import { ProcessCapturePaymentManager } from "./managers/process-capture-payment.manager";
import { ProcessCaptureSimulationManager } from "./managers/process-capture-simulation.manager";
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
    public readonly configuration: ProcessCaptureConfigurationManager,
    public readonly documents: ProcessCaptureDocumentManager,
    public readonly familyIncomeStatement: ProcessCaptureFamilyIncomeStatementManager,
    public readonly legalRepresentative: ProcessCaptureLegalRepresentativeManager,
    public readonly patient: ProcessCapturePatientManager,
    public readonly payment: ProcessCapturePaymentManager,
    public readonly simulation: ProcessCaptureSimulationManager
  ) {
    super(store, state, [
      configuration,
      documents,
      familyIncomeStatement,
      legalRepresentative,
      patient,
      payment,
      simulation
    ]);
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.documents.form.updateValueAndValidity();
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
    ProcessCaptureDocumentManager,
    ProcessCaptureFamilyIncomeStatementManager,
    ProcessCaptureLegalRepresentativeManager,
    ProcessCapturePatientManager,
    ProcessCapturePaymentManager,
    ProcessCaptureSimulationManager
  ]),

  provideEffects(ProcessCaptureEffects),
  provideState(FEATURE_NAME, reducer)
];

