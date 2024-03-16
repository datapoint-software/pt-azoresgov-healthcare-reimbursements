import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../process-capture.constants";
import { ProcessCaptureState } from "./process-capture.state";
import { PaymentMethod } from "../../../enums/payment-method.enum";
import { PaymentReceiver } from "../../../enums/payment-receiver.enum";

export const deleteFamilyIncomeStatement = createAction(
  `${FEATURE_ACTION_PREFIX}/delete-family-income-statement`
);

export const deleteFamilyIncomeStatementComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/delete-family-income-statement-complete`,
  props<{
    payload: {
      processRowVersionId: string;
    };
  }>()
);

export const deleteLegalRepresentative = createAction(
  `${FEATURE_ACTION_PREFIX}/delete-legal-representative`
);

export const deleteLegalRepresentativeComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/delete-legal-representative-complete`,
  props<{
    payload: {
      processRowVersionId: string;
    };
  }>()
);

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
    payload: ProcessCaptureState
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const writeConfiguration = createAction(
  `${FEATURE_ACTION_PREFIX}/write-configuration`,
  props<{
    payload: {
      debounce: boolean;
      configuration: {
        machadoJosephEnabled: boolean;
        documentIssueDateBypassEnabled: boolean;
        reimbursementLimitBypassEnabled: boolean;
        unemploymentEnabled: boolean;
      };
    };
  }>()
);

export const writeConfigurationComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-configuration-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processConfigurationRowVersionId: string;
    };
  }>()
);

export const writeFamilyIncomeStatement = createAction(
  `${FEATURE_ACTION_PREFIX}/write-family-income-statement`,
  props<{
    payload: {
      debounce: boolean;
      familyIncomeStatement: {
        year: number;
        accessCode?: string;
        familyMemberCount: number;
        familyIncome: number;
      };
    };
  }>()
);

export const writeFamilyIncomeStatementComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-family-income-statement-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processPatientFamilyIncomeStatementRowVersionId: string;
    };
  }>()
);

export const writeLegalRepresentative = createAction(
  `${FEATURE_ACTION_PREFIX}/write-legal-representative`,
  props<{
    payload: {
      debounce: boolean;
      legalRepresentative: {
        name: string;
        taxNumber: string;
        emailAddress?: string;
        faxNumber?: string;
        mobileNumber?: string;
        phoneNumber?: string;
      };
    };
  }>()
);

export const writeLegalRepresentativeComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-legal-representative-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processPatientLegalRepresentativeRowVersionId: string;
    };
  }>()
);

export const writePatient = createAction(
  `${FEATURE_ACTION_PREFIX}/write-patient`,
  props<{
    payload: {
      debounce: boolean;
      patient: {
        addressLine1: string;
        addressLine2?: string;
        addressLine3?: string;
        postalCode: string;
        postalCodeArea: string;
        emailAddress?: string;
        faxNumber?: string;
        mobileNumber?: string;
        phoneNumber?: string;
      }
    };
  }>()
);

export const writePatientComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-patient-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processPatientRowVersionId: string;
    };
  }>()
);

export const writePayment = createAction(
  `${FEATURE_ACTION_PREFIX}/write-payment`,
  props<{
    payload: {
      debounce: boolean;
      payment: {
        method: PaymentMethod;
        receiver: PaymentReceiver;
        iban?: string;
        swift?: string;
      }
    };
  }>()
);

export const writePaymentComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-payment-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processPaymentConfigurationRowVersionId: string;
      processPaymentWireTransferConfigurationRowVersionId?: string;
    };
  }>()
);
