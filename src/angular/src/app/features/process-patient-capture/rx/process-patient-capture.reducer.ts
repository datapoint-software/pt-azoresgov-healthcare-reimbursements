import { createReducer, on } from "@ngrx/store";
import { ProcessPatientCaptureState } from "./process-patient-capture.state";
import { configure, dispose, init } from "./process-patient-capture.actions";

export const reducer = createReducer(

  (undefined as unknown as ProcessPatientCaptureState),

  on(dispose, () => (undefined as unknown as ProcessPatientCaptureState)),
  on(init, () => (undefined as unknown as ProcessPatientCaptureState)),
  on(configure, (_, { payload }) => ({ ...payload }))
)
