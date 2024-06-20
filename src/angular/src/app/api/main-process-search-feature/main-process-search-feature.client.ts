import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessSearchFeatureProcessSearchModel, MainProcessSearchFeatureProcessSearchResultModel } from "@app/api/main-process-search-feature/main-process-search-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-search";

@Injectable()
export class MainProcessSearchFeatureClient {

  public async searchProcesses(model: MainProcessSearchFeatureProcessSearchModel): Promise<MainProcessSearchFeatureProcessSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessSearchFeatureProcessSearchResultModel>(
        `${baseAddress}/search-processes`,
        model
      )
    )
  };

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
