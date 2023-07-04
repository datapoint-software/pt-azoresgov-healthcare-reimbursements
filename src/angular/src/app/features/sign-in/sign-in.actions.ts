import { createAction, props } from "@ngrx/store";
import { featureName } from "./sign-in.constants";
import { SignInConfigurePayload, SignInErrorPayload, SignInPayload } from "./sign-in.payloads";

export const configure = createAction(
  `${featureName}.configure`,
  props<{ payload: SignInConfigurePayload }>()
);

export const dispose = createAction(
  `${featureName}.dispose`
);

export const error = createAction(
  `${featureName}.error`,
  props<{ payload: SignInErrorPayload }>()
);

export const init = createAction(
  `${featureName}.init`
);

export const redirect = createAction(
  `${featureName}.redirect`
);

export const signIn = createAction(
  featureName,
  props<{ payload: SignInPayload }>()
);
