import { createAction, props } from "@ngrx/store";
import { featureName } from "./loading-overlay.constants";
import { LoadingOverlayDequeuePayload, LoadingOverlayEnqueuePayload, LoadingOverlayTaskPayload } from "./loading-overlay.payloads";

export const dequeue = createAction(
  `${featureName}.dequeue`,
  props<{ payload: LoadingOverlayDequeuePayload }>()
);

export const enqueue = createAction(
  `${featureName}.enqueue`,
  props<{ payload: LoadingOverlayEnqueuePayload }>()
);

export const hide = createAction(
  `${featureName}.hide`
);

export const reset = createAction(
  `${featureName}.reset`
);

export const show = createAction(
  `${featureName}.show`
);

export const task = createAction(
  `${featureName}.task`,
  props<{ payload: LoadingOverlayTaskPayload }>()
);
