import { ProcessCaptureOptionsEntityResultModel, ProcessCaptureOptionsPatientResultModel, ProcessCaptureOptionsProcessResultModel } from "../../../clients/process-capture/process-capture.models";

export interface ProcessCaptureState {
  entity: ProcessCaptureOptionsEntityResultModel;
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel;
  process: ProcessCaptureOptionsProcessResultModel;
  writting: boolean;
}
