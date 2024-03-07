import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { ProcessSearchOptionsResultModel, ProcessSearchResultModel } from "./process-search.models";
import { ProcessStatus } from "../../enums/process-status.enum";

@Injectable()
export class ProcessSearchClient extends Client {

  protected override baseAddress: string = '/api/features/process-search';

  readonly getOptions = () =>

    this.get<ProcessSearchOptionsResultModel>('/');

  readonly search = (entityId?: string, filter?: string, status?: ProcessStatus, skip?: number, take?: number) =>

    this.get<ProcessSearchResultModel>('/search',
      { params: { filter, entityId, status, skip, take }});

}

export const provideProcessSearchClient = () => [
  makeEnvironmentProviders([
    ProcessSearchClient
  ])
];
