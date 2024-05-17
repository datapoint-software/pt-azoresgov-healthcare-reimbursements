import { EntityNature } from "../../../../app.enums";

export type ProcessCreationEntitySelectionSearchModel = {
  filter: string;
  skip?: number;
  take?: number;
};

export type ProcessCreationEntitySelectionSearchResultEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
};

export type ProcessCreationEntitySelectionSearchResultModel = {
  totalMatchCount: number;
  entityIds: string[];
  entities: ProcessCreationEntitySelectionSearchResultEntityModel[];
};
