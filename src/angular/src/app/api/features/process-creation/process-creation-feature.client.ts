import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { firstValueFrom } from "rxjs";
import { ProcessCreationFeatureOptionsResultModel } from "./process-creation-feature-client.abstractions";

const baseAddress = "/api/features/process-creation";

@Injectable()
export class ProcessCreationFeatureClient {

  public async getOptions(): Promise<ProcessCreationFeatureOptionsResultModel> {
    return await firstValueFrom(
      this._httpClient.get<ProcessCreationFeatureOptionsResultModel>(
        baseAddress
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
