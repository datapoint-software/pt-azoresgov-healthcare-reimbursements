import { EntityNature } from "../../enums/entity-nature.enum";
import { Gender } from "../../enums/gender.enum";
import { ProcessStatus } from "../../enums/process-status.enum";

export interface ProcessCaptureConfigurationModel {
  rowVersionId: string;
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

export interface ProcessCaptureOptionsPatientResultModel {
  id: string;
  rowVersionId: string;
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
  entity: ProcessCaptureOptionsEntityResultModel;
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel;
  process: ProcessCaptureOptionsProcessResultModel;
}

export interface ProcessCapturePatientModel {
  processRowVersionId: string;
  patientRowVersionId: string;
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
  patientRowVersionId: string;
}

export interface ProcessCaptureConfigurationResultModel {
  rowVersionId: string;
}
