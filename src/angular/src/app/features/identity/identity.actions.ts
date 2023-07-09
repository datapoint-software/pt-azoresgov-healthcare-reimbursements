import { createAction, props } from "@ngrx/store";

const prefix = '@app/identity';

export const authenticate = createAction(
  `${prefix}/authenticate`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number,
      refreshToken: string,
      persistent: boolean
    }
  }>()
);

export const authenticateRefresh = createAction(
  `${prefix}/authenticate?refresh`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number,
      refreshToken: string,
      persistent: boolean
    }
  }>()
);

export const authenticateWriteSecrets = createAction(
  `${prefix}/authenticate?write-secrets`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number,
      refreshToken: string,
      persistent: boolean
    }
  }>()
);

export const authenticateConfigureSecrets = createAction(
  `${prefix}/authenticate?configure-secrets`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number,
      refreshToken: string,
      persistent: boolean
    }
  }>()
);

export const dispose = createAction(
  `${prefix}/dispose`
);

export const init = createAction(
  `${prefix}/init`
);

export const initClearSecrets = createAction(
  `${prefix}/init?clear-secrets`
);

export const initConfigureWithoutSecrets = createAction(
  `${prefix}/init?configure-without-secrets`
);

export const initConfigureWithSecrets = createAction(
  `${prefix}/init?configure-with-secrets`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number;
      persistent: boolean;
      refreshToken: string,
    }
  }>()
);

export const initReadSecrets = createAction(
  `${prefix}/init?read-secrets`
);

export const initRefresh = createAction(
  `${prefix}/init?refresh`,
  props<{
    payload: {
      accessToken: string,
      refreshToken: string,
      persistent: boolean
    }
  }>()
);

export const redirectToSignIn = createAction(
  `${prefix}/redirect-to-sign-in`
);

export const refresh = createAction(
  `${prefix}/refresh`
);

export const refreshRefresh = createAction(
  `${prefix}/refresh?refresh`,
  props<{
    payload: {
      accessToken: string,
      refreshToken: string
    }
  }>()
);

export const refreshClearSecrets = createAction(
  `${prefix}/refresh?clear-secrets`
);

export const refreshWriteSecrets = createAction(
  `${prefix}/refresh?write-secrets`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number;
      refreshToken: string,
    }
  }>()
);

export const refreshConfigureSecrets = createAction(
  `${prefix}/refresh?configure-secrets`,
  props<{
    payload: {
      accessToken: string,
      accessTokenExpiration: number;
      refreshToken: string,
    }
  }>()
);

export const scheduleRefresh = createAction(
  `${prefix}/schedule-refresh`,
  props<{
    payload: {
      accessTokenExpiration: number;
    }
  }>()
);
