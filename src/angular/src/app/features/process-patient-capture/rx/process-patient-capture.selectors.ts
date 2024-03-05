import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProcessPatientCaptureState } from "./process-patient-capture.state";
import { FEATURE_NAME } from "../process-patient-capture.constants";

export const state = createFeatureSelector<ProcessPatientCaptureState>(FEATURE_NAME);

export const process = createSelector(state, state => state.process);

export const processNumber = createSelector(process, process => process.number);
