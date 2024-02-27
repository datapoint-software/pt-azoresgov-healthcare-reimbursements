import { createFeatureSelector, createSelector } from "@ngrx/store";
import { IdentityState } from "./identity.state";
import { FEATURE_NAME } from "../identity.constants";

export const state = createFeatureSelector<IdentityState>(FEATURE_NAME);

export const user = createSelector(state, state => state.user!);

export const userId = createSelector(user, user => user.id);

export const userName = createSelector(user, user => user.name);

export const userEmailAddress = createSelector(user, user => user.emailAddress);
