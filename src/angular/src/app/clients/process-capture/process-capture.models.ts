import { EntityNature } from "../../enums/entity-nature.enum";
import { Gender } from "../../enums/gender.enum";
import { ProcessStatus } from "../../enums/process-status.enum";

export interface ProcessCaptureConfigurationModel {
  processRowVersionId: string;
  processConfigurationRowVersionId?: string;
  machadoJosephEnabled: boolean;
  documentIssueDateBypassEnabled: boolean;
  reimbursementLimitBypassEnabled: boolean;
}

export interface ProcessCaptureConfigurationResultModel {
  processRowVersionId: string;
  processConfigurationRowVersionId: string;
}

export interface ProcessCaptureOptionsConfigurationResultModel {
  rowVersionId?: string;
  machadoJosephEnabled: boolean;
  documentIssueDateBypassEnabled: boolean;
  reimbursementLimitBypassEnabled: boolean;
}

export interface ProcessCaptureOptionsEntityResultModel {
  id: string;
  rowVersionId: string;
  code: string;
  name: string;
  nature: EntityNature;
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

export interface ProcessCaptureOptionsProcessResultModel {
  id: string;
  rowVersionId: string;
  number: string;
  status: ProcessStatus;
}

export interface ProcessCaptureOptionsResultModel {
  configuration?: ProcessCaptureOptionsConfigurationResultModel;
  entity: ProcessCaptureOptionsEntityResultModel;
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel;
  patientLegalRepresentative?: ProcessCaptureOptionsPatientLegalRepresentativeResultModel;
  process: ProcessCaptureOptionsProcessResultModel;
}

export interface ProcessCapturePatientModel {
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
