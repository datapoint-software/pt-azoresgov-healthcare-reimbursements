import { FEATURE_ACTION_PREFIX } from "../environment.constants";
import { createAction, props } from "@ngrx/store";
import { EnvironmentNature } from "../../../enums/environment-nature.enum";

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: {
      nature: EnvironmentNature;
      productVersion: string;
    };
  }>()
);
