import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ErrorState } from "./error.state";
import { FEATURE_NAME } from "../error.constants";

export const state = createFeatureSelector<ErrorState>(FEATURE_NAME);

export const id = createSelector(state, state => state.id);

export const correlationId = createSelector(state, state => state.correlationId);

export const message = createSelector(state, state => state.message);

export const stackTrace = createSelector(state, state => state.stackTrace);

export const status = createSelector(state, state => state.status);
