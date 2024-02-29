import { EntityNature } from "../../enums/entity-nature.enum";

export interface ProcessCreationEntitySearchEntityResultModel {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
}

export interface ProcessCreationEntitySearchResultModel {
  entities: ProcessCreationEntitySearchEntityResultModel[];
  matches: string[];
  totalMatchCount: number;
}
