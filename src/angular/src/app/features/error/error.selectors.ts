import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ErrorState } from "./error.state";
import { featureName } from "./error.constants";

export const state = createFeatureSelector<ErrorState>(featureName);

export const id = createSelector(state, state => state.id);
export const message = createSelector(state, state => state.message);
export const status = createSelector(state, state => state.status);
export const statusCode = createSelector(status, status => status?.code);
export const statusMessage = createSelector(status, status => status?.message);
