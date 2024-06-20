import { EntityNature, ProcessStatus } from "@app/enums";

export type MainProcessSearchFeatureEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export type MainProcessSearchFeaturePatientModel = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber: string;
  name: string;
  death?: string;
}

export type MainProcessSearchFeatureProcessModel = {
  id: string;
  rowVersionId: string;
  entityId: string;
  patientId: string;
  number: string;
  status: ProcessStatus;
  creation: string;
}

export type MainProcessSearchFeatureProcessSearchModel = {
  filter?: string;
  useFullSearchCriteria: boolean;
  skip?: number;
  take?: number;
}

export type MainProcessSearchFeatureProcessSearchResultModel = {
  totalMatchCount: number;
  processIds: string[];
  entities: MainProcessSearchFeatureEntityModel[];
  patients: MainProcessSearchFeaturePatientModel[];
  processes: MainProcessSearchFeatureProcessModel[];
}
