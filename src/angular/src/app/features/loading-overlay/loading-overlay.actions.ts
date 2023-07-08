import { createAction, props } from "@ngrx/store";

const prefix = '@app/loading-overlay';

export const dequeue = createAction(
  `${prefix}/dequeue`,
  props<{
    payload: {
      id: string
    }
  }>()
);

export const enqueue = createAction(
  `${prefix}/enqueue`,
  props<{
    payload: {
      id: string
    }
  }>()
);

export const reset = createAction(
  `${prefix}/reset`
);
