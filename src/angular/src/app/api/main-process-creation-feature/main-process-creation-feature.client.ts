import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCreationFeatureOptionsResultModel } from "@app/api/main-process-creation-feature/main-process-creation-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-creation";

@Injectable()
export class MainProcessCreationFeatureClient {

  public async getOptions(): Promise<MainProcessCreationFeatureOptionsResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationFeatureOptionsResultModel>(
        `${baseAddress}/get-options`,
        null
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
