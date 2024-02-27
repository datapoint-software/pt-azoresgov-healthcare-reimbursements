import { createFeatureSelector, createSelector } from "@ngrx/store";
import { LoadingOverlayState } from "./loading-overlay.state";
import { FEATURE_NAME } from "../loading-overlay.constants";

export const state = createFeatureSelector<LoadingOverlayState>(FEATURE_NAME);

export const items = createSelector(state, state => state.items);
