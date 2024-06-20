import { FormControl, FormGroup } from "@angular/forms";
import { EntityNature, ProcessStatus } from "@app/enums";

export type MainProcessCreationFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCreationFeatureEntitySearchForm = FormGroup<{
  filter: FormControl<string | null>;
}>;

export type MainProcessCreationFeatureEntitySearchResult = {
  totalMatchCount: number;
  entityIds: string[];
};

export type MainProcessCreationFeaturePatient = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber: string | null;
  name: string;
  birth: string | null;
  death: string | null;
  faxNumber: string | null;
  mobileNumber: string | null;
  phoneNumber: string | null;
  emailAddress: string | null;
  external: boolean;
};

export type MainProcessCreationFeaturePatientSearchForm = FormGroup<{
  filter: FormControl<string | null>;
  useFullSearchCriteria: FormControl<boolean | null>;
}>;

export type MainProcessCreationFeaturePatientSearchResult = {
  totalMatchCount: number;
  patientIds: string[];
};

export type MainProcessCreationFeatureProcess = {
  id: string;
  rowVersionId: string;
  number: string;
  status: ProcessStatus;
  creation: string;
};

export enum MainProcessCreationFeatureStep {
  EntitySelection,
  PatientSelection,
  Confirmation
};
