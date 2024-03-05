import { EntityNature } from "../../enums/entity-nature.enum";
import { Gender } from "../../enums/gender.enum";
import { ProcessStatus } from "../../enums/process-status.enum";

export interface ProcessPatientCaptureOptionsEntityResultModel {
  id: string;
  rowVersionId: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export interface ProcessPatientCaptureOptionsPatientResultModel {
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

export interface ProcessPatientCaptureOptionsProcessResultModel {
  id: string;
  rowVersionId: string;
  number: string;
  status: ProcessStatus;
}

export interface ProcessPatientCaptureOptionsResultModel {
  entity: ProcessPatientCaptureOptionsEntityResultModel;
  parentEntity?: ProcessPatientCaptureOptionsEntityResultModel;
  patient: ProcessPatientCaptureOptionsPatientResultModel;
  process: ProcessPatientCaptureOptionsProcessResultModel;
}
