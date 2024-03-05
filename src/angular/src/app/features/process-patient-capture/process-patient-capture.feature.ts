import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { dispose, init } from "./rx/process-patient-capture.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-patient-capture.constants";
import { ProcessPatientCaptureEffects } from "./rx/process-patient-capture.effects";
import { ProcessPatientCaptureState } from "./rx/process-patient-capture.state";
import { provideEffects } from "@ngrx/effects";
import { reducer } from "./rx/process-patient-capture.reducer";
import { state } from "./rx/process-patient-capture.selectors";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";

@Injectable()
export class ProcessPatientCaptureFeature extends Feature<ProcessPatientCaptureState> {

  constructor(store: Store) {
    super(store, state);
  }

  protected override dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return init({
      payload: {
        processId: activatedRoute.paramMap.get('processId')!
      }
    });
  }
}

export const provideProcessPatientCaptureFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessPatientCaptureFeature
  ]),

  provideEffects(ProcessPatientCaptureEffects),
  provideState(FEATURE_NAME, reducer)
];
