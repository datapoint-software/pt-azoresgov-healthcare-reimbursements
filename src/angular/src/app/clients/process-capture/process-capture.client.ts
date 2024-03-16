import { Client } from "../api.client";
import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { ProcessCaptureConfigurationModel, ProcessCaptureConfigurationResultModel, ProcessCaptureLegalRepresentativeDeleteModel, ProcessCaptureLegalRepresentativeDeleteResultModel, ProcessCaptureOptionsResultModel, ProcessCapturePatientModel, ProcessCapturePatientResultModel, ProcessCaptureLegalRepresentativeModel, ProcessCaptureLegalRepresentativeResultModel, ProcessCaptureFamilyIncomeStatementModel, ProcessCaptureFamilyIncomeStatementResultModel } from "./process-capture.models";

@Injectable()
export class ProcessCaptureClient extends Client {

  protected override baseAddress: string = '/api/features/process-capture';

  readonly deleteLegalRepresentative = (processId: string, model: ProcessCaptureLegalRepresentativeDeleteModel) =>

    this.post<ProcessCaptureLegalRepresentativeDeleteModel, ProcessCaptureLegalRepresentativeDeleteResultModel>(
      '/:processId/delete-legal-representative',
      model,
      { path: { processId }}
    );

  readonly getOptions = (processId: string) =>

    this.get<ProcessCaptureOptionsResultModel>('/:processId', { path: { processId }});

  readonly writeConfiguration = (processId: string, model: ProcessCaptureConfigurationModel) =>

    this.post<ProcessCaptureConfigurationModel, ProcessCaptureConfigurationResultModel>(
      '/:processId/configuration',
      model,
      { path: { processId }});

  readonly writeFamilyIncomeStatement = (model: ProcessCaptureFamilyIncomeStatementModel) =>

    this.post<ProcessCaptureFamilyIncomeStatementModel, ProcessCaptureFamilyIncomeStatementResultModel>(
      '/write-family-income-statement',
      model)

  readonly writeLegalRepresentative = (processId: string, model: ProcessCaptureLegalRepresentativeModel) =>

    this.post<ProcessCaptureLegalRepresentativeModel, ProcessCaptureLegalRepresentativeResultModel>(
      '/:processId/write-legal-representative',
      model,
      { path: { processId }});

  readonly writePatient = (processId: string, model: ProcessCapturePatientModel) =>

    this.post<ProcessCapturePatientModel, ProcessCapturePatientResultModel>(
      '/:processId/patient',
      model,
      { path: { processId }});

}

export const provideProcessCaptureClient = () => [
  makeEnvironmentProviders([
    ProcessCaptureClient
  ])
];
