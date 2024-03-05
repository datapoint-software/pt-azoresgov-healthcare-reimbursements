import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProcessCreationState } from "./process-creation.state";
import { FEATURE_NAME } from "../process-creation.constants";

export const state = createFeatureSelector<ProcessCreationState>(FEATURE_NAME);

export const complete = createSelector(state, state => state.complete);

export const step = createSelector(state, state => state.step);

export const steps = createSelector(state, state => state.steps);

export const entities = createSelector(state, state => state.entities);

export const entityId = createSelector(state, state => state.entityId);

export const entitySearchResult = createSelector(state, state => state.entitySearchResult);

export const entitySearchResultEntityIds = createSelector(entitySearchResult, entitySearchResult => entitySearchResult?.entityIds);

export const entitySearchResultTotalMatchCount = createSelector(entitySearchResult, entitySearchResult => entitySearchResult?.totalMatchCount);

export const patientId = createSelector(state, state => state.patientId);

export const patients = createSelector(state, state => state.patients);

export const patientSearchResult = createSelector(state, state => state.patientSearchResult);

export const patientSearchResultEntityIds = createSelector(patientSearchResult, patientSearchResult => patientSearchResult?.patientIds);

export const patientSearchResultTotalMatchCount = createSelector(patientSearchResult, patientSearchResult => patientSearchResult?.totalMatchCount);

export const process = createSelector(state, state => state.process);

export const processId = createSelector(process, process => process?.id);
