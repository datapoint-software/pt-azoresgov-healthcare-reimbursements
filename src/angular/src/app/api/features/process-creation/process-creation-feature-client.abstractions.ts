import { EntityNature } from "../../../app.enums";

export interface ProcessCreationFeatureOptionsResultEntityModel {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
}

export interface ProcessCreationFeatureOptionsResultModel {
  entitySelectionEnabled: boolean;
  entities?: ProcessCreationFeatureOptionsResultEntityModel[];
  entityId?: string;
}
