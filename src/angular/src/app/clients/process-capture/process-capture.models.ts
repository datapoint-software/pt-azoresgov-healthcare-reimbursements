import { EntityNature } from "../../enums/entity-nature.enum";
import { Gender } from "../../enums/gender.enum";
import { PaymentMethod } from "../../enums/payment-method.enum";
import { PaymentReceiver } from "../../enums/payment-receiver.enum";
import { ProcessStatus } from "../../enums/process-status.enum";

export interface ProcessCaptureBankResultModel {
  name: string;
  swiftCode: string;
}

export interface ProcessCaptureConfigurationModel {
  processId: string;
  processRowVersionId: string;
  processConfigurationRowVersionId?: string;
  machadoJosephEnabled: boolean;
  documentIssueDateBypassEnabled: boolean;
  reimbursementLimitBypassEnabled: boolean;
  unemploymentEnabled: boolean;
}

export interface ProcessCaptureConfigurationResultModel {
  processId: string;
  processRowVersionId: string;
  processConfigurationRowVersionId: string;
}

export interface ProcessCaptureFamilyIncomeStatementDeleteModel {
  processId: string;
  processRowVersionId: string;
  processPatientFamilyIncomeStatementRowVersionId: string;
}

export interface ProcessCaptureFamilyIncomeStatementDeleteResultModel {
  processRowVersionId: string;
}

export interface ProcessCaptureFamilyIncomeStatementModel {
  processId: string;
  processRowVersionId: string;
  processPatientFamilyIncomeStatementRowVersionId?: string;
  year: number;
  accessCode?: string;
  familyMemberCount: number;
  familyIncome: number;
}

export interface ProcessCaptureFamilyIncomeStatementResultModel {
  processRowVersionId: string;
  processPatientFamilyIncomeStatementRowVersionId: string;
}

export interface ProcessCaptureOptionsConfigurationResultModel {
  documentIssueDateBypassEnabled: boolean;
  machadoJosephEnabled: boolean;
  reimbursementLimitBypassEnabled: boolean;
  unemploymentEnabled: boolean;
  rowVersionId?: string;
  writting: boolean;
}

export interface ProcessCaptureOptionsEntityResultModel {
  id: string;
  rowVersionId: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export interface ProcessCaptureOptionsFamilyIncomeStatementResultModel {
  rowVersionId?: string;
  year: number;
  accessCode?: string;
  familyMemberCount: number;
  familyIncome: number;
}

export interface ProcessCaptureOptionsIasConfigurationResultModel {
  year: number;
  amount: number;
}

export interface ProcessCaptureOptionsPatientLegalRepresentativeResultModel {
  rowVersionId?: string;
  name: string;
  taxNumber: string;
  emailAddress?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
}

export interface ProcessCaptureOptionsPatientResultModel {
  rowVersionId?: string;
  name: string;
  birth?: string;
  gender?: Gender;
  healthNumber: string;
  taxNumber: string;
  addressLine1: string;
  addressLine2?: string;
  addressLine3?: string;
  postalCode?: string;
  postalCodeArea?: string;
  emailAddress?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  death?: string;
}

export interface ProcessCaptureOptionsPaymentResultModel {
  processPaymentConfigurationRowVersionId?: string;
  processPaymentWireTransferConfigurationRowVersionId?: string;
  method: PaymentMethod;
  receiver: PaymentReceiver;
  iban?: string;
  swift?: string;
}

export interface ProcessCaptureOptionsProcessResultModel {
  id: string;
  rowVersionId: string;
  number: string;
  status: ProcessStatus;
}

export interface ProcessCaptureOptionsResultModel {
  configuration?: ProcessCaptureOptionsConfigurationResultModel;
  entity: ProcessCaptureOptionsEntityResultModel;
  familyIncomeStatement?: ProcessCaptureOptionsFamilyIncomeStatementResultModel;
  iasConfiguration: ProcessCaptureOptionsIasConfigurationResultModel;
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel;
  patientLegalRepresentative?: ProcessCaptureOptionsPatientLegalRepresentativeResultModel;
  payment?: ProcessCaptureOptionsPaymentResultModel;
  process: ProcessCaptureOptionsProcessResultModel;
}

export interface ProcessCapturePatientModel {
  processId: string;
  processRowVersionId: string;
  processPatientRowVersionId?: string;
  addressLine1: string;
  addressLine2?: string;
  addressLine3?: string;
  postalCode?: string;
  postalCodeArea?: string;
  emailAddress?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
}

export interface ProcessCapturePatientResultModel {
  processRowVersionId: string;
  processPatientRowVersionId: string;
}

export interface ProcessCaptureLegalRepresentativeModel {
  processId: string;
  processRowVersionId: string;
  processPatientLegalRepresentativeId?: string;
  name: string;
  taxNumber: string;
  emailAddress?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
}

export interface ProcessCaptureLegalRepresentativeResultModel {
  processRowVersionId: string;
  processPatientLegalRepresentativeRowVersionId: string;
}

export interface ProcessCaptureLegalRepresentativeDeleteModel {
  processId: string;
  processRowVersionId: string;
  processPatientLegalRepresentativeRowVersionId: string;
}

export interface ProcessCaptureLegalRepresentativeDeleteResultModel {
  processRowVersionId: string;
}

export interface ProcessCapturePaymentDeleteModel {
  processId: string;
  processRowVersionId: string;
  processPaymentConfigurationRowVersionId: string;
  processPaymentWireTransferConfigurationRowVersionId?: string;
}

export interface ProcessCapturePaymentDeleteResultModel {
  processRowVersionId: string;
}

export interface ProcessCapturePaymentModel {
  processId: string;
  processRowVersionId: string;
  processPaymentConfigurationRowVersionId?: string;
  processPaymentWireTransferConfigurationRowVersionId?: string;
  method: PaymentMethod;
  receiver: PaymentReceiver;
  iban?: string;
  swift?: string;
}

export interface ProcessCapturePaymentResultModel {
  processRowVersionId: string;
  processPaymentConfigurationRowVersionId: string;
  processPaymentWireTransferConfigurationRowVersionId?: string;
}
