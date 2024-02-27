import { createFeatureSelector, createSelector } from "@ngrx/store";
import { EnvironmentState } from "./environment.state";
import { FEATURE_NAME } from "../environment.constants";

export const state = createFeatureSelector<EnvironmentState>(FEATURE_NAME);

export const nature = createSelector(state, state => state.nature);

export const productVersion = createSelector(state, state => state.productVersion);
