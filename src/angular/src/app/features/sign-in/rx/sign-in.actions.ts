import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../sign-in.constants";
import { SignInState } from "./sign-in.state";
import { ErrorResponseModel } from "../../../clients/api.models";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`,
  props<{
    payload: {
      redirectUrl?: string;
    };
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{ payload: SignInState }>()
);

export const error = createAction(
  `${FEATURE_ACTION_PREFIX}/error`,
  props<{
    payload: ErrorResponseModel;
  }>()
);

export const redirect = createAction(
  `${FEATURE_ACTION_PREFIX}/redirect`
);

export const signIn = createAction(
  `${FEATURE_ACTION_PREFIX}/sign-in`,
  props<{
    payload: {
      emailAddress: string;
      password: string;
      persistent: boolean;
    };
  }>()
);
