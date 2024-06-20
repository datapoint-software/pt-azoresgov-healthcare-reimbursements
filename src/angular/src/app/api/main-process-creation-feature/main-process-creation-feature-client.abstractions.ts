import { EntityNature, ProcessStatus } from "@app/enums";

export type MainProcessCreationFeatureConfirmationModel = {
  entityId: string;
  entityRowVersionId: string;
  patientId: string;
  patientRowVersionId: string;
};

export type MainProcessCreationFeatureConfirmationResultModel = {
  process: MainProcessCreationFeatureProcessModel;
};

export type MainProcessCreationFeatureEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCreationFeatureEntitySearchModel = {
  filter: string;
  skip?: number;
  take?: number;
};

export type MainProcessCreationFeatureEntitySearchResultModel = {
  totalMatchCount: number;
  entityIds: string[];
  entities: MainProcessCreationFeatureEntityModel[];
};

export type MainProcessCreationFeatureOptionsResultEntitySelectionModel = {
  entityId: string;
  entities: MainProcessCreationFeatureEntityModel[];
};

export type MainProcessCreationFeatureOptionsResultModel = {
  entitySelection?: MainProcessCreationFeatureOptionsResultEntitySelectionModel;
};

export type MainProcessCreationFeaturePatientModel = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber?: string;
  name: string;
  birth?: string;
  death?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  external: boolean;
};

export type MainProcessCreationFeaturePatientSearchModel = {
  entityId: string;
  entityRowVersionId: string;
  filter: string;
  useFullSearchCriteria: boolean;
  skip?: number;
  take?: number;
};

export type MainProcessCreationFeaturePatientSearchResultModel = {
  totalMatchCount: number;
  patientIds: string[];
  entities: MainProcessCreationFeatureEntityModel[];
  patients: MainProcessCreationFeaturePatientModel[];
};

export type MainProcessCreationFeatureProcessModel = {
  id: string;
  rowVersionId: string;
  number: string;
  status: ProcessStatus;
  creation: string;
};
