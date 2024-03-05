import { ProcessPatientCaptureOptionsEntityResultModel, ProcessPatientCaptureOptionsPatientResultModel, ProcessPatientCaptureOptionsProcessResultModel } from "../../../clients/process-patient-capture/process-patient-capture.models";

export interface ProcessPatientCaptureState {
  entity: ProcessPatientCaptureOptionsEntityResultModel;
  parentEntity?: ProcessPatientCaptureOptionsEntityResultModel;
  patient: ProcessPatientCaptureOptionsPatientResultModel;
  process: ProcessPatientCaptureOptionsProcessResultModel;
}
