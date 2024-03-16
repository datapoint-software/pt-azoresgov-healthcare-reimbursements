import { createFeatureSelector, createSelector } from "@ngrx/store";
import { FEATURE_NAME } from "../process-capture.constants";
import { ProcessCaptureState } from "./process-capture.state";

export const state = createFeatureSelector<ProcessCaptureState>(FEATURE_NAME);

export const configuration = createSelector(state, state => state.configuration);

export const configurationRowVersionId = createSelector(configuration, configuration => configuration?.rowVersionId);

export const configurationWritting = createSelector(configuration, configuration => configuration?.writting);

export const entity = createSelector(state, state => state.entity);

export const legalRepresentative = createSelector(state, state => state.legalRepresentative);

export const legalRepresentativeRowVersionId = createSelector(legalRepresentative, patientLegalRepresentative => patientLegalRepresentative?.rowVersionId);

export const legalRepresentativeWritting = createSelector(legalRepresentative, patientLegalRepresentative => patientLegalRepresentative?.writting);

export const patient = createSelector(state, state => state.patient);

export const patientRowVersionId = createSelector(patient, patient => patient.rowVersionId);

export const patientHealthNumber = createSelector(patient, patient => patient.healthNumber);

export const patientName = createSelector(patient, patient => patient.name);

export const patientTaxNumber = createSelector(patient, patient => patient.taxNumber);

export const patientWritting = createSelector(patient, patient => patient.writting);

export const parentEntity = createSelector(state, state => state.parentEntity);

export const process = createSelector(state, state => state.process);

export const processId = createSelector(process, process => process.id);

export const processRowVersionId = createSelector(process, process => process.rowVersionId);

export const processNumber = createSelector(process, process => process.number);

export const writting = createSelector(
  configurationWritting,
  patientWritting,
  (configuration, patient) => configuration || patient
);
