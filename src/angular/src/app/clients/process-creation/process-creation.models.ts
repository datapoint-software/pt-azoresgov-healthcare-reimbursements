import { EntityNature } from "../../enums/entity-nature.enum";

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

export interface ProcessCreationOptionsResultModel {
  entities?: ProcessCreationEntityResultModel[];
  entityId?: string;
}
