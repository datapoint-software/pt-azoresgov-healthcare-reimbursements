import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { ProcessCreationEntitySearchResultModel, ProcessCreationOptionsResultModel, ProcessCreationPatientSearchResultModel } from "./process-creation.models";

@Injectable()
export class ProcessCreationClient extends Client {

  protected override baseAddress: string = '/api/features/process-creation';

  readonly getOptions = () =>

    this.get<ProcessCreationOptionsResultModel>('/');

  readonly searchEntities = (filter?: string, skip?: number, take?: number) =>

    this.get<ProcessCreationEntitySearchResultModel>('/entities', {
      params: { filter, skip, take }
    });

  readonly searchPatients = (entityId: string, filter?: string, skip?: number, take?: number) =>

    this.get<ProcessCreationPatientSearchResultModel>('/entities/:entityId/patients', {
      params: { filter, skip, take },
      path: { entityId }
    });

}

export const provideProcessCreationClient = () => [
  makeEnvironmentProviders([
    ProcessCreationClient
  ])
];
