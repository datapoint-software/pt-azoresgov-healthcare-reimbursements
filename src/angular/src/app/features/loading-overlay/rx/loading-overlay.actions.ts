import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../loading-overlay.constants";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const enqueue = createAction(
  `${FEATURE_ACTION_PREFIX}/enqueue`,
  props<{
    payload: {
      id: string
    }
  }>()
);

export const dequeue = createAction(
  `${FEATURE_ACTION_PREFIX}/dequeue`,
  props<{
    payload: {
      id: string
    }
  }>()
);
