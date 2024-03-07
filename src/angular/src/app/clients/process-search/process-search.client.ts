import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { ProcessSearchOptionsResultModel, ProcessSearchResultModel } from "./process-search.models";

@Injectable()
export class ProcessSearchClient extends Client {

  protected override baseAddress: string = '/api/features/process-search';

  readonly getOptions = () =>

    this.get<ProcessSearchOptionsResultModel>('/');

  readonly search = (entityId?: string, filter?: string, skip?: number, take?: number) =>

    this.get<ProcessSearchResultModel>('/search',
      { params: { filter, entityId, skip, take }});

}

export const provideProcessSearchClient = () => [
  makeEnvironmentProviders([
    ProcessSearchClient
  ])
];
