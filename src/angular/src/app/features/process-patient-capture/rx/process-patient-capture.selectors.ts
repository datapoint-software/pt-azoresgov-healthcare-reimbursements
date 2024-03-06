import { createFeatureSelector, createSelector } from "@ngrx/store";
import { ProcessPatientCaptureState } from "./process-patient-capture.state";
import { FEATURE_NAME } from "../process-patient-capture.constants";

export const state = createFeatureSelector<ProcessPatientCaptureState>(FEATURE_NAME);

export const patient = createSelector(state, state => state.patient);

export const patientHealthNumber = createSelector(patient, patient => patient.healthNumber);

export const patientName = createSelector(patient, patient => patient.name);

export const patientTaxNumber = createSelector(patient, patient => patient.taxNumber);

export const process = createSelector(state, state => state.process);

export const processNumber = createSelector(process, process => process.number);
