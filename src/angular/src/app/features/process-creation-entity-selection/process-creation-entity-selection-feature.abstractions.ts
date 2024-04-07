import { FormControl, FormGroup } from "@angular/forms";
import { EntityNature } from "../../app.enums";

export type ProcessCreationEntitySelectionFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type ProcessCreationEntitySelectionFeatureForm = FormGroup<{
  filter: FormControl<string | null>;
}>;

export type ProcessCreationEntitySelectionFeatureOptions = {
  enabled: boolean;
  entities: ProcessCreationEntitySelectionFeatureEntity[];
  entityId: string | null;
};
