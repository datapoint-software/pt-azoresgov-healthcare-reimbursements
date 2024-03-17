import { createReducer, on } from "@ngrx/store";
import { clearBankResult, configure, deleteFamilyIncomeStatement, deleteFamilyIncomeStatementComplete, deleteLegalRepresentative, deleteLegalRepresentativeComplete, dispose, init, searchBankComplete, writeConfiguration, writeConfigurationComplete, writeFamilyIncomeStatement, writeFamilyIncomeStatementComplete, writeLegalRepresentative, writeLegalRepresentativeComplete, writePatient, writePatientComplete, writePayment, writePaymentComplete } from "./process-capture.actions";
import { ProcessCaptureState } from "./process-capture.state";

export const reducer = createReducer(

  (undefined as unknown as ProcessCaptureState),

  on(dispose, () => (undefined as unknown as ProcessCaptureState)),
  on(init, () => (undefined as unknown as ProcessCaptureState)),

  on(configure, (_, { payload }) => ({ ...payload })),

  on(clearBankResult, (state) => ({
    ...state,
    bank: undefined
  })),

  on(searchBankComplete, (state, { payload }) => ({
    ...state,
    bank: {
      ...payload
    }
  })),

  on(deleteFamilyIncomeStatement, (state) => ({
    ...state,
    familyIncomeStatement: state.familyIncomeStatement && {
      ...state.familyIncomeStatement,
      writting: true
    }
  })),

  on(deleteFamilyIncomeStatementComplete, (state, { payload }) => ({
    ...state,
    familyIncomeStatement: undefined,
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    }
  })),

  on(deleteLegalRepresentative, (state) => ({
    ...state,
    legalRepresentative: state.legalRepresentative && {
      ...state.legalRepresentative,
      writting: true
    }
  })),

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
  })),

  on(writePayment, (state, { payload }) => ({
    ...state,
    payment: {
      ...state.payment,
      ...payload.payment,
      writting: true
    }
  })),

  on(writePaymentComplete, (state, { payload }) => ({
    ...state,
    process: {
      ...state.process,
      rowVersionId: payload.processRowVersionId
    },
    payment: {
      ...state.payment!,
      processPaymentConfigurationRowVersionId: payload.processPaymentConfigurationRowVersionId,
      processPaymentWireTransferConfigurationRowVersionId: payload.processPaymentWireTransferConfigurationRowVersionId,
      writting: false
    }
  }))
);
