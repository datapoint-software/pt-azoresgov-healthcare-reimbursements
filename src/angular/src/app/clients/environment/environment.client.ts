import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { EnvironmentResultModel } from "./environment.models";

@Injectable()
export class EnvironmentClient extends Client {

  protected override readonly baseAddress: string = '/api/features/environment';

  readonly getEnvironment = () =>
    this.get<EnvironmentResultModel>('/');

}

export const provideEnvironmentClient = () => [
  makeEnvironmentProviders([
    EnvironmentClient
  ])
];
