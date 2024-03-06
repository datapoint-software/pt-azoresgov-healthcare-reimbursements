import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { ProcessCaptureOptionsResultModel, ProcessCapturePatientModel, ProcessCapturePatientResultModel } from "./process-capture.models";

@Injectable()
export class ProcessCaptureClient extends Client {

  protected override baseAddress: string = '/api/features/process-capture';

  readonly getOptions = (processId: string) =>

    this.get<ProcessCaptureOptionsResultModel>('/:processId', { path: { processId }});

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
