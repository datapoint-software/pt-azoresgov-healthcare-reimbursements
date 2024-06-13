import { EntityNature } from "@app/enums";

export type MainProcessCreationEntitySelectionSearchModel = {
  filter: string;
  skip?: number;
  take?: number;
};

export type MainProcessCreationEntitySelectionSearchResultEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCreationEntitySelectionSearchResultModel = {
  totalMatchCount: number;
  entityIds: string[];
  entities: MainProcessCreationEntitySelectionSearchResultEntityModel[];
};
