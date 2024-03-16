import { createReducer, on } from "@ngrx/store";
import { ProcessCaptureState } from "./process-capture.state";
import { configure, deleteLegalRepresentativeComplete, dispose, init, writeConfiguration, writeConfigurationComplete, writeFamilyIncomeStatement, writeFamilyIncomeStatementComplete, writeLegalRepresentative, writeLegalRepresentativeComplete, writePatient, writePatientComplete } from "./process-capture.actions";

export const reducer = createReducer(

  (undefined as unknown as ProcessCaptureState),

  on(dispose, () => (undefined as unknown as ProcessCaptureState)),
  on(init, () => (undefined as unknown as ProcessCaptureState)),

  on(configure, (_, { payload }) => ({ ...payload })),

  on(deleteLegalRepresentativeComplete, (state, { payload }) => ({
    ...state,
    legalRepresentative: undefined,
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  })),

  on(writeConfiguration, (state, { payload }) => ({
    ...state,
    configuration: {
      ...state.configuration,
      ...payload.configuration,
      writting: true
    }
  })),

  on(writeFamilyIncomeStatement, (state, { payload }) => ({
    ...state,
    familyIncomeStatement: {
      ...state.familyIncomeStatement,
      ...payload.familyIncomeStatement!,
      writting: true
    }
  })),

  on(writeFamilyIncomeStatementComplete, (state, { payload }) => ({
    ...state,
    familyIncomeStatement: {
      ...state.familyIncomeStatement!,
      rowVersionId: payload.processPatientFamilyIncomeStatementRowVersionId,
      writting: false
    },
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  })),

  on(writePatient, (state, { payload }) => ({
    ...state,
    patient: {
      ...state.patient,
      ...payload.patient,
      writting: true
    }
  })),

  on(writeConfigurationComplete, (state, { payload }) => ({
    ...state,
    configuration: {
      ...state.configuration!,
      rowVersionId: payload.processConfigurationRowVersionId,
      writting: false
    },
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  })),

  on(writeLegalRepresentative, (state, { payload }) => ({
    ...state,
    legalRepresentative: {
      ...state.legalRepresentative,
      ...payload.legalRepresentative,
      writting: true
    }
  })),

  on(writeLegalRepresentativeComplete, (state, { payload }) => ({
    ...state,
    legalRepresentative: {
      ...state.legalRepresentative!,
      rowVersionId: payload.processPatientLegalRepresentativeRowVersionId,
      writting: false
    },
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  })),

  on(writePatientComplete, (state, { payload }) => ({
    ...state,
    patient: {
      ...state.patient,
      rowVersionId: payload.processPatientRowVersionId,
      writting: false
    },
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  }))
);
