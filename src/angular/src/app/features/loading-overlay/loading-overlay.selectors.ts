import { createFeatureSelector, createSelector } from "@ngrx/store";
import { featureName } from "./loading-overlay.constants";
import { LoadingOverlayState } from "./loading-overlay.state";

export const state = createFeatureSelector<LoadingOverlayState>(featureName);

export const tasks = createSelector(
  state,
  state => state.tasks
);

export const tasksAsArray = createSelector(
  tasks,
  tasks => Object.values(tasks)
);

export const visible = createSelector(
  state,
  state => Object.values(state.tasks).length > 0
);
