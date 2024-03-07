import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProcessSearchState } from "./process-search.state";
import { FEATURE_NAME } from "../process-search.constants";

export const state = createFeatureSelector<ProcessSearchState>(FEATURE_NAME);

export const entities = createSelector(state, (state) => state.entities);

export const searchResult = createSelector(state, (state) => state.searchResult);
