import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { SignInFeatureOptionsModel } from "./sign-in-feature-client.abstractions";

const baseAddress = "/api/features/sign-in";

@Injectable()
export class SignInFeatureClient {

  public async getOptions(): Promise<SignInFeatureOptionsModel> {
    return await firstValueFrom(
      this._httpClient.get<SignInFeatureOptionsModel>(
        baseAddress
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
