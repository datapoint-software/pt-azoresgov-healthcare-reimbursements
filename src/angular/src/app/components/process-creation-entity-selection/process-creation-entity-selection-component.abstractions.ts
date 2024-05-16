import { EntityNature } from "../../app.enums";

export type ProcessCreationEntitySelectionComponentSearchResultEntity = {
  id: string;
  rowVersionId: string;
  parentEntity: Readonly<ProcessCreationEntitySelectionComponentSearchResultEntity> | null;
  code: string;
  name: string;
  nature: EntityNature;
};
