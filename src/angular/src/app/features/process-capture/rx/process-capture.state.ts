import { ProcessCaptureOptionsConfigurationResultModel, ProcessCaptureOptionsEntityResultModel, ProcessCaptureOptionsFamilyIncomeStatementResultModel, ProcessCaptureOptionsPatientLegalRepresentativeResultModel, ProcessCaptureOptionsPatientResultModel, ProcessCaptureOptionsProcessResultModel } from "../../../clients/process-capture/process-capture.models";

export interface ProcessCaptureState {
  configuration?: ProcessCaptureOptionsConfigurationResultModel & { writting: boolean };
  entity: ProcessCaptureOptionsEntityResultModel;
  familyIncomeStatement?: ProcessCaptureOptionsFamilyIncomeStatementResultModel & { writting: boolean; };
  legalRepresentative?: ProcessCaptureOptionsPatientLegalRepresentativeResultModel & { writting: boolean };
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel & { writting: boolean };
  process: ProcessCaptureOptionsProcessResultModel;
}
