import { createFeatureSelector, createSelector } from "@ngrx/store";
import { featureName } from "./process-creation.constants";
import { ProcessCreationState } from "./process-creation.state";

export const state = createFeatureSelector<ProcessCreationState>(featureName);

export const entitySelection = createSelector(state, s => s.entitySelection);

export const entitySelectionEnabled = createSelector(entitySelection, es => es.enabled);
