import { EntityNature } from "../../enums/entity-nature.enum";
import { Gender } from "../../enums/gender.enum";

export interface ProcessCreationEntityResultModel {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export interface ProcessCreationEntitySearchResultModel {
  entities: ProcessCreationEntityResultModel[];
  entityIds: string[];
  totalMatchCount: number;
}

export interface ProcessCreationModel {
  entityId: string;
  entityRowVersionId: string;
  patientId: string;
  patientRowVersionId: string;
}

export interface ProcessCreationResultModel {
  id: string;
  rowVersionId: string;
  number: string;
}

export interface ProcessCreationOptionsResultModel {
  entities?: ProcessCreationEntityResultModel[];
  entityId?: string;
}

export interface ProcessCreationPatientResultModel {
  id: string;
  rowVersionId: string;
  name: string;
  birth?: string;
  gender?: Gender;
  healthNumber: string;
  taxNumber: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  death?: string;
}

export interface ProcessCreationPatientSearchResultModel {
  patientIds: string[];
  patients: ProcessCreationPatientResultModel[];
  totalMatchCount: number;
}
