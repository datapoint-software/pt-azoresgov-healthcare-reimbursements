import { Client } from "../api.client";
import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { ProcessCaptureConfigurationModel, ProcessCaptureConfigurationResultModel, ProcessCaptureDeleteLegalRepresentativeModel, ProcessCaptureDeleteLegalRepresentativeResultModel, ProcessCaptureOptionsResultModel, ProcessCapturePatientModel, ProcessCapturePatientResultModel, ProcessCaptureWriteLegalRepresentativeModel, ProcessCaptureWriteLegalRepresentativeResultModel } from "./process-capture.models";

@Injectable()
export class ProcessCaptureClient extends Client {

  protected override baseAddress: string = '/api/features/process-capture';

  readonly deleteLegalRepresentative = (processId: string, model: ProcessCaptureDeleteLegalRepresentativeModel) =>

    this.post<ProcessCaptureDeleteLegalRepresentativeModel, ProcessCaptureDeleteLegalRepresentativeResultModel>(
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

    readonly writeLegalRepresentative = (processId: string, model: ProcessCaptureWriteLegalRepresentativeModel) =>

      this.post<ProcessCaptureWriteLegalRepresentativeModel, ProcessCaptureWriteLegalRepresentativeResultModel>(
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
