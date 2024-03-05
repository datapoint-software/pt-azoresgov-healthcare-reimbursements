import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { ProcessPatientCaptureOptionsResultModel } from "./process-patient-capture.models";

@Injectable()
export class ProcessPatientCaptureClient extends Client {

  protected override baseAddress: string = '/api/features/process-patient-capture';

  readonly getOptions = (processId: string) =>

    this.get<ProcessPatientCaptureOptionsResultModel>('/:processId', { path: { processId }});

}

export const provideProcessPatientCaptureClient = () => [
  makeEnvironmentProviders([
    ProcessPatientCaptureClient
  ])
];
