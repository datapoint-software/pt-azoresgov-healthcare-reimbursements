import { FormControl, FormGroup } from "@angular/forms";
import { EntityNature, ProcessStatus } from "@app/enums";

export type MainProcessSearchFeatureProcessSearchForm = FormGroup<{
  filter: FormControl<string | null>;
  useFullSearchCriteria: FormControl<boolean | null>;
}>;

export type MainProcessSearchFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessSearchFeaturePatient = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber: string;
  name: string;
  death: string | null;
};

export type MainProcessSearchFeatureProcess = {
  id: string;
  rowVersionId: string;
  entityId: string;
  patientId: string;
  number: string;
  status: ProcessStatus;
  creation: string;
};

export type MainProcessSearchFeatureProcessSearch = {
  filter: string | null;
  useFullSearchCriteria: boolean;
  skip: number | null;
  take: number | null;
};

export type MainProcessSearchFeatureProcessSearchResult = {
  totalMatchCount: number;
  processIds: Set<string>;
};
