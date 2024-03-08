import { createReducer, on } from "@ngrx/store";
import { ProcessCaptureState } from "./process-capture.state";
import { configure, debounceWritting, dispose, init, writeConfigurationComplete, writePatient, writePatientComplete } from "./process-capture.actions";

export const reducer = createReducer(

  (undefined as unknown as ProcessCaptureState),

  on(dispose, () => (undefined as unknown as ProcessCaptureState)),
  on(init, () => (undefined as unknown as ProcessCaptureState)),

  on(configure, (_, { payload }) => ({ ...payload })),

  on(debounceWritting, (state) => ({
    ...state,
    writting: true
  })),

  on(writeConfigurationComplete, (state, { payload }) => ({
    ...state,
    process: {
      ...state.process,
      rowVersionId: payload.rowVersionId
    },
    writting: false
  })),

  on(writePatientComplete, (state, { payload }) => ({
    ...state,
    patient: {
      ...state.patient,
      rowVersionId: payload.patientRowVersionId
    },
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    },
    writting: false
  }))
);
