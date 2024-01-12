import { createAction, props } from "@ngrx/store";

const prefix = '@environment';

export const configure = createAction(
  `${prefix}/configure`,
  props<{
    payload: {
      production: boolean;
      debugSymbols: boolean;
      fileVersion: string;
      productVersion: string;
    }
  }>()
);

export const init = createAction(
  `${prefix}/init`
);

export const dispose = createAction(
  `${prefix}/dispose`
);
