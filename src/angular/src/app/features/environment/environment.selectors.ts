import { createFeatureSelector, createSelector } from "@ngrx/store";
import { EnvironmentState } from "./environment.state";
import { featureName } from "./environment.constants";

export const state = createFeatureSelector<EnvironmentState>(featureName);

export const production = createSelector(
  state,
  state => state.production
);

export const debugSymbols = createSelector(
  state,
  state => state.debugSymbols
);

export const fileVersion = createSelector(
  state,
  state => state.fileVersion
);

export const productVersion = createSelector(
  state,
  state => state.productVersion
);
