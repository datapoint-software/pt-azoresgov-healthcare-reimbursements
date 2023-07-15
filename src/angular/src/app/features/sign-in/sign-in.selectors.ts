import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SignInState } from "./sign-in.state";
import { featureName } from "./sign-in.constants";

export const state = createFeatureSelector<SignInState>(featureName);

export const authentication = createSelector(state, state => state.authentication);
export const authenticationEnabled = createSelector(authentication, authentication => authentication.enabled);
export const authenticationError = createSelector(authentication, authentication => authentication.error);
export const authenticationPersistentEnabled = createSelector(authentication, authentication => authentication.persistentEnabled);

export const redirectUrl = createSelector(state, state => state.redirectUrl);
