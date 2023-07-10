import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IdentityState } from "./identity.state";
import { featureName } from "./identity.constants";

export const state = createFeatureSelector<IdentityState>(featureName);

export const secrets = createSelector(state, state => state.secrets);

export const secretsAccessToken = createSelector(secrets, secrets => secrets?.accessToken);

export const secretsAccessTokenExpiration = createSelector(secrets, secrets => secrets?.accessTokenExpiration);

export const secretsPersistent = createSelector(secrets, secrets => secrets?.persistent);

export const secretsRefreshToken = createSelector(secrets, secrets => secrets?.refreshToken);
