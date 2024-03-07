import { Gender } from "../../enums/gender.enum";
import { ProcessStatus } from "../../enums/process-status.enum";

export interface ProcessSearchEntityResultModel {
  id: string;
  code: string;
  name: string;
}

export interface ProcessSearchOptionsEntityResultModel {
  id: string;
  name: string;
}

export interface ProcessSearchOptionsResultModel {
  entities: ProcessSearchOptionsEntityResultModel[];
}

export interface ProcessSearchPatientResultModel {
  id: string;
  name: string;
  gender?: Gender;
  healthNumber: string;
  taxNumber: string;
  birth?: string;
  death?: string;
}

export interface ProcessSearchProcessResultModel {
  id: string;
  entityId: string;
  patientId: string;
  number: string;
  status: ProcessStatus;
  creation: string;
  expiration?: string;
  touch: string;
}

export interface ProcessSearchResultModel {
  entities: ProcessSearchEntityResultModel[];
  patients: ProcessSearchPatientResultModel[];
  processes: ProcessSearchProcessResultModel[];
  totalMatchCount: number;
}
