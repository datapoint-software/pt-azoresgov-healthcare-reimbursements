import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../process-capture.constants";
import { ProcessCaptureState } from "./process-capture.state";
import { ProcessCaptureOptionsResultModel } from "../../../clients/process-capture/process-capture.models";

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`,
  props<{
    payload: {
      processId: string;
    };
  }>()
);

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: ProcessCaptureState
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const debounceWritting = createAction(
  `${FEATURE_ACTION_PREFIX}/debounce-writting`
);

export const preConfigure = createAction(
  `${FEATURE_ACTION_PREFIX}/pre-configure`,
  props<{
    payload: ProcessCaptureOptionsResultModel;
  }>()
);

export const writeConfiguration = createAction(
  `${FEATURE_ACTION_PREFIX}/write-configuration`,
  props<{
    payload: {
      machadoJosephEnabled: boolean;
      documentIssueDateBypassEnabled: boolean;
      reimbursementLimitBypassEnabled: boolean;
    };
  }>()
);

export const writeConfigurationComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-configuration-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processConfigurationRowVersionId: string;
    };
  }>()
);

export const writePatient = createAction(
  `${FEATURE_ACTION_PREFIX}/write-patient`,
  props<{
    payload: {
      addressLine1: string;
      addressLine2?: string;
      addressLine3?: string;
      postalCode: string;
      postalCodeArea: string;
      emailAddress?: string;
      faxNumber?: string;
      mobileNumber?: string;
      phoneNumber?: string;
    };
  }>()
);

export const writePatientComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/write-patient-complete`,
  props<{
    payload: {
      processRowVersionId: string;
      processPatientRowVersionId: string;
    };
  }>()
);
