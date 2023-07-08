import { createAction, props } from "@ngrx/store";

const prefix = '@app/error';

export const configure = createAction(
  `${prefix}/configure`,
  props<{
    payload: {
      id?: string;
      message?: string;
      status?: {
        code: number;
        message: string;
      }
    }
  }>()
);

export const init = createAction(
  `${prefix}/init`,
  props<{
    payload: {
      id?: string;
      message?: string;
      statusCode?: number;
    }
  }>()
);

export const dispose = createAction(
  `${prefix}/dispose`
);
