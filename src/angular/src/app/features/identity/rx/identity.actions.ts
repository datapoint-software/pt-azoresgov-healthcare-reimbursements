import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../identity.constants";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: {
      user?: {
        id: string;
        name: string;
        emailAddress: string;
      };
    };
  }>()
);
