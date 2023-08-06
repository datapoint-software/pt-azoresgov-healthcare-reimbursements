import { createAction, props } from "@ngrx/store";

const prefix = '@app/process-creation';

export const dispose = createAction(
  `${prefix}.dispose`
);

export const init = createAction(
  `${prefix}/init`
);

export const initConfigure = createAction(
  `${init.type}?configure`,
  props<{
    payload: {
      entitySelection: {
        enabled: boolean;
      };
    };
  }>()
);

export const selectEntity = createAction(
  `${prefix}/select-entity`,
  props<{
    payload: {
      entityId: string
    };
  }>()
);

export const selectEntityComplete = createAction(
  `${selectEntity.type}?complete`
);
