import { Client } from "../api.client";
import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { SignInModel, SignInOptionsResultModel, SignInResultModel } from "./sign-in.models";

@Injectable()
export class SignInClient extends Client {

  protected override baseAddress: string = '/api/features/sign-in';

  readonly getOptions = () =>
    this.get<SignInOptionsResultModel>('/');

  readonly signIn = (model: SignInModel) =>
    this.post<SignInModel, SignInResultModel>('/', model);
}

export const provideSignInClient = () => [
  makeEnvironmentProviders([
    SignInClient
  ])
];
