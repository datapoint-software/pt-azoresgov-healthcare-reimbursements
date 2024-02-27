import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { Client } from "../api.client";
import { IdentityResultModel } from "./identity.models";

@Injectable()
export class IdentityClient extends Client {

  protected override baseAddress: string = '/api/features/identity';

  readonly getIdentity = () =>
    this.get<IdentityResultModel>(null);
}

export const provideIdentityClient = () => [
  makeEnvironmentProviders([
    IdentityClient
  ])
];
