import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IdentityFeatureRefreshResultModel } from "./identity-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/identity";

@Injectable()
export class IdentityFeatureClient {

  public async refresh(): Promise<IdentityFeatureRefreshResultModel> {
    return await firstValueFrom(
      this._httpClient.post<IdentityFeatureRefreshResultModel>(
        `${baseAddress}/refresh`,
        null
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
