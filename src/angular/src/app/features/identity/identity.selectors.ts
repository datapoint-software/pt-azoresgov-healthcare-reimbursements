import { createFeatureSelector, createSelector } from "@ngrx/store";
import { featureName } from "./identity.constants";
import { IdentityState } from "./identity.state";

export const state = createFeatureSelector<IdentityState>(featureName);

export const claims = createSelector(state, state => state.claims);

export const entities = createSelector(claims, claims => claims?.entities);

export const permissions = createSelector(claims, claims => claims?.permissions);

export const user = createSelector(claims, claims => claims?.user);

export const userId = createSelector(user, user => user?.id);

export const userName = createSelector(user, user => user?.name);
