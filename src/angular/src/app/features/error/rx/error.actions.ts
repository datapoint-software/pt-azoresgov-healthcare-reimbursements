import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../error.constants";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`,
  props<{
    payload: {
      id?: string;
      correlationId?: string;
      message?: string;
      stackTrace?: string;
      statusCode?: number;
    };
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: {
      id?: string;
      correlationId?: string;
      message?: string;
      stackTrace?: string;
      status?: {
        code: number;
        message: string;
      };
    };
  }>()
)
