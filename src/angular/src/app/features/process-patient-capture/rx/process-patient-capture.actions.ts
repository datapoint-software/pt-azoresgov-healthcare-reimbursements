import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../process-patient-capture.constants";
import { ProcessPatientCaptureState } from "./process-patient-capture.state";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`,
  props<{
    payload: {
      processId: string;
    };
  }>()
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: ProcessPatientCaptureState;
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);
