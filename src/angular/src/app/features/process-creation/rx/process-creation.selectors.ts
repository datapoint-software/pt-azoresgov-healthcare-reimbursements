import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProcessCreationState } from "./process-creation.state";
import { FEATURE_NAME } from "../process-creation.constants";

export const state = createFeatureSelector<ProcessCreationState>(FEATURE_NAME);

export const step = createSelector(state, state => state.step);

export const steps = createSelector(state, state => state.steps);

export const entitySearchResult = createSelector(state, state => state.entitySearchResult!);

export const entitySearchResultEntities = createSelector(entitySearchResult, entitySearchResult => entitySearchResult.entities);

export const entitySearchResultMatches = createSelector(entitySearchResult, entitySearchResult => entitySearchResult.matches);

export const entitySearchResultTotalMatchCount = createSelector(entitySearchResult, entitySearchResult => entitySearchResult.totalMatchCount);
