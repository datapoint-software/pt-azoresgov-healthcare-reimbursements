import { createAction, props } from "@ngrx/store";
import { ErrorModel } from "../../app.models";
import { prefix } from "./sign-in.constants";

export const dispose = createAction(
  `${prefix}/dispose`
);

export const init = createAction(
  `${prefix}/init`,
  props<{
    payload: {
      redirectUrl?: string
    }
  }>()
);

export const initGetOptions = createAction(
  `${prefix}/init?get-options`,
  props<{
    payload: {
      redirectUrl?: string
    }
  }>()
);

export const initConfigure = createAction(
  `${prefix}/init?configure`,
  props<{
    payload: {
      authentication: {
        enabled: boolean,
        persistentEnabled: boolean,
      },
      redirectUrl?: string
    }
  }>()
);

export const redirect = createAction(
  `${prefix}/redirect`
);

export const signIn = createAction(
  `${prefix}/sign-in`,
  props<{
    payload: {
      emailAddress: string,
      password: string,
      persistent: boolean
    }
  }>()
);

export const signInSignIn = createAction(
  `${prefix}/sign-in?sign-in`,
  props<{
    payload: {
      emailAddress: string,
      password: string,
      persistent: boolean
    }
  }>()
);

export const signInError = createAction(
  `${prefix}/sign-in?error`,
  props<{
    payload: ErrorModel
  }>()
);
