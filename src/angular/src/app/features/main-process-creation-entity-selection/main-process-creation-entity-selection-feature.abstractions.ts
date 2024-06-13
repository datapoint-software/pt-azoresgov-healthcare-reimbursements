import { FormControl, FormGroup } from "@angular/forms";
import { EntityNature } from "@app/enums";

export type MainProcessCreationEntitySelectionFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCreationEntitySelectionFeatureForm = FormGroup<{
  filter: FormControl<string | null>;
}>;

export type MainProcessCreationEntitySelectionFeatureOptions = {
  enabled: boolean;
  entities: MainProcessCreationEntitySelectionFeatureEntity[];
  entityId: string | null;
};

export type MainProcessCreationEntitySelectionFeatureSearchResult = {
  entityIds: string[];
  totalMatchCount: number;
};
