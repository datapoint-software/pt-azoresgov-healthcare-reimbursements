import { createFeatureSelector } from "@ngrx/store";
import { ProcessPatientCaptureState } from "./process-patient-capture.state";
import { FEATURE_NAME } from "../process-patient-capture.constants";

export const state = createFeatureSelector<ProcessPatientCaptureState>(FEATURE_NAME);
