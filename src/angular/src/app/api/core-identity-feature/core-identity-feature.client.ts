import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CoreIdentityFeatureRefreshResultModel } from "@app/api/core-identity-feature/core-identity-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/core-identity";

@Injectable()
export class CoreIdentityFeatureClient {

  public async refresh(): Promise<CoreIdentityFeatureRefreshResultModel> {
    return await firstValueFrom(
      this._httpClient.post<CoreIdentityFeatureRefreshResultModel>(
        `${baseAddress}/refresh`,
        null
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
