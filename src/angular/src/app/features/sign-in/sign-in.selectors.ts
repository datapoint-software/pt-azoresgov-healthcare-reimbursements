import { createFeatureSelector, createSelector } from "@ngrx/store";
import { featureName } from "./sign-in.constants";
import { SignInState } from "./sign-in.state";

export const state = createFeatureSelector<SignInState>(featureName);

export const authentication = createSelector(
  state,
  state => state.authentication
);

export const authenticationEnabled = createSelector(
  authentication,
  authentication => authentication.enabled
);

export const authenticationPersistentEnabled = createSelector(
  authentication,
  authentication => authentication.persistentEnabled
);

export const error = createSelector(
  state,
  state => state.error
);

export const redirectUrl = createSelector(
  state,
  state => state.redirectUrl
);
