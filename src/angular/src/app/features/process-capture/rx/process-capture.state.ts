import { ProcessCaptureBankResultModel, ProcessCaptureOptionsConfigurationResultModel, ProcessCaptureOptionsEntityResultModel, ProcessCaptureOptionsFamilyIncomeStatementResultModel, ProcessCaptureOptionsIasConfigurationResultModel, ProcessCaptureOptionsPatientLegalRepresentativeResultModel, ProcessCaptureOptionsPatientResultModel, ProcessCaptureOptionsPaymentResultModel, ProcessCaptureOptionsProcessResultModel } from "../../../clients/process-capture/process-capture.models";

export interface ProcessCaptureState {
  bank?: ProcessCaptureBankResultModel;
  configuration?: ProcessCaptureOptionsConfigurationResultModel & { writting: boolean };
  entity: ProcessCaptureOptionsEntityResultModel;
  familyIncomeStatement?: ProcessCaptureOptionsFamilyIncomeStatementResultModel & { writting: boolean; };
  iasConfiguration: ProcessCaptureOptionsIasConfigurationResultModel;
  legalRepresentative?: ProcessCaptureOptionsPatientLegalRepresentativeResultModel & { writting: boolean };
  parentEntity?: ProcessCaptureOptionsEntityResultModel;
  patient: ProcessCaptureOptionsPatientResultModel & { writting: boolean };
  payment?: ProcessCaptureOptionsPaymentResultModel & { writting: boolean };
  process: ProcessCaptureOptionsProcessResultModel;
}
