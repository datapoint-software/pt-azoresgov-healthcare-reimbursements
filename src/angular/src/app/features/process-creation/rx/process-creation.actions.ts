import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../process-creation.constants";
import { ProcessCreationState } from "./process-creation.state";
import { ProcessCreationEntitySearchResultModel, ProcessCreationPatientSearchResultModel } from "../../../clients/process-creation/process-creation.models";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: ProcessCreationState
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const next = createAction(
  `${FEATURE_ACTION_PREFIX}/next`
);

export const previous = createAction(
  `${FEATURE_ACTION_PREFIX}/previous`
);

export const step = createAction(
  `${FEATURE_ACTION_PREFIX}/step`,
  props<{
    payload: number;
  }>()
);

export const searchEntities = createAction(
  `${FEATURE_ACTION_PREFIX}/search-entities`,
  props<{
    payload: {
      filter?: string;
    };
  }>()
);

export const searchEntitiesComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/search-entities-complete`,
  props<{
    payload: ProcessCreationEntitySearchResultModel
  }>()
);

export const searchPatients = createAction(
  `${FEATURE_ACTION_PREFIX}/search-patients`,
  props<{
    payload: {
      filter?: string;
    };
  }>()
);

export const searchPatientsComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/search-patients-complete`,
  props<{
    payload: ProcessCreationPatientSearchResultModel;
  }>()
);

export const selectEntity = createAction(
  `${FEATURE_ACTION_PREFIX}/select-entity`,
  props<{
    payload: {
      id: string;
    };
  }>()
);

export const selectPatient = createAction(
  `${FEATURE_ACTION_PREFIX}/select-patient`,
  props<{
    payload: {
      id: string;
    };
  }>()
);
