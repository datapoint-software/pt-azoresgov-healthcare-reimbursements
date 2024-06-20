import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCreationFeatureConfirmationModel, MainProcessCreationFeatureConfirmationResultModel, MainProcessCreationFeatureEntitySearchModel, MainProcessCreationFeatureEntitySearchResultModel, MainProcessCreationFeatureOptionsResultModel, MainProcessCreationFeaturePatientSearchModel, MainProcessCreationFeaturePatientSearchResultModel } from "@app/api/main-process-creation-feature/main-process-creation-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-creation";

@Injectable()
export class MainProcessCreationFeatureClient {

  public async confirm(model: MainProcessCreationFeatureConfirmationModel): Promise<MainProcessCreationFeatureConfirmationResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationFeatureConfirmationResultModel>(
        `${baseAddress}/confirm`,
        model
      )
    );
  }

  public async getOptions(): Promise<MainProcessCreationFeatureOptionsResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationFeatureOptionsResultModel>(
        `${baseAddress}/get-options`,
        null
      )
    );
  }

  public async searchEntities(model: MainProcessCreationFeatureEntitySearchModel): Promise<MainProcessCreationFeatureEntitySearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationFeatureEntitySearchResultModel>(
        `${baseAddress}/search-entities`,
        model
      )
    );
  }

  public async searchPatients(model: MainProcessCreationFeaturePatientSearchModel): Promise<MainProcessCreationFeaturePatientSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationFeaturePatientSearchResultModel>(
        `${baseAddress}/search-patients`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}

}
