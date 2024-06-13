import { EntityNature } from "@app/enums";

export type MainProcessCreationFeatureOptionsResultEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export type MainProcessCreationFeatureOptionsResultModel = {
  entitySelectionEnabled: boolean;
  entities?: MainProcessCreationFeatureOptionsResultEntityModel[];
  entityId?: string;
}
