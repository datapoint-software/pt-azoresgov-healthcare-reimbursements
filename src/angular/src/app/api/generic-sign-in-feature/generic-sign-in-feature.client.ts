import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { GenericSignInFeatureOptionsModel, GenericSignInFeatureSignInModel, GenericSignInFeatureSignInResultModel } from "@app/api/generic-sign-in-feature/generic-sign-in-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/generic-sign-in";

@Injectable()
export class GenericSignInFeatureClient {

  public async getOptions(): Promise<GenericSignInFeatureOptionsModel> {
    return await firstValueFrom(
      this._httpClient.post<GenericSignInFeatureOptionsModel>(
        `${baseAddress}/get-options`,
        null
      )
    );
  }

  public async signIn(model: GenericSignInFeatureSignInModel): Promise<GenericSignInFeatureSignInResultModel> {
    return await firstValueFrom(
      this._httpClient.post<GenericSignInFeatureSignInResultModel>(
        `${baseAddress}/sign-in`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
