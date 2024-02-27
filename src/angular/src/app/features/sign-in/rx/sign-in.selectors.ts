import { createFeatureSelector, createSelector } from "@ngrx/store";
import { SignInState } from "./sign-in.state";
import { FEATURE_NAME } from "../sign-in.constants";

export const state = createFeatureSelector<SignInState>(FEATURE_NAME);

export const error = createSelector(state, state => state.error);

export const method = createSelector(state, state => state.method);

export const methods = createSelector(state, state => state.methods);

export const basicMethod = createSelector(methods, methods => methods.basic!);

export const basicMethodPersistentSessionsEnabled = createSelector(basicMethod, basicMethod => basicMethod.persistentSessionsEnabled);

export const redirectUrl = createSelector(state, state => state.redirectUrl);
