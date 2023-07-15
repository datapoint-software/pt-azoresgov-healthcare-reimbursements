import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { SignInModel, SignInOptionsResultModel, SignInResultModel } from "./sign-in.models";

const baseAddress = '/api/features/sign-in';

@Injectable()
export class SignInClient {

  constructor(
    private readonly httpClient: HttpClient
  ) {}

  public getOptions() {
    return this.httpClient.get<SignInOptionsResultModel>(
      `${baseAddress}/options`
    );
  }

  public signIn(model: SignInModel) {
    return this.httpClient.post<SignInResultModel>(
      baseAddress,
      model
    );
  }
}
