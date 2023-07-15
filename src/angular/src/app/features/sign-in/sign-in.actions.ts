import { createAction, props } from "@ngrx/store";
import { ErrorModel } from "../../app.models";

const prefix = '@app/sign-in';

export const dispose = createAction(
  `${prefix}/dispose`
);

export const init = createAction(
  `${prefix}/init`,
  props<{
    payload: {
      redirectUrl?: string;
    };
  }>()
);

export const initGetOptions = createAction(
  `${prefix}/init?get-options`,
  props<{
    payload: {
      redirectUrl?: string;
    };
  }>()
);

export const initConfigure = createAction(
  `${prefix}/init?configure`,
  props<{
    payload: {
      authentication: {
        enabled: boolean;
        persistentEnabled: boolean;
      };
    };
  }>()
);

export const signIn = createAction(
  `${prefix}/sign-in`,
  props<{
    payload: {
      emailAddress: string;
      password: string;
      persistent: boolean;
    };
  }>()
);

export const signInPost = createAction(
  `${prefix}/sign-in?post`,
  props<{
    payload: {
      emailAddress: string;
      password: string;
      persistent: boolean;
    };
  }>()
);

export const signInPostError = createAction(
  `${prefix}/sign-in?post-error`,
  props<{
    payload: ErrorModel;
  }>()
);

export const signInRedirect = createAction(
  `${prefix}/signIn?redirect`
);
