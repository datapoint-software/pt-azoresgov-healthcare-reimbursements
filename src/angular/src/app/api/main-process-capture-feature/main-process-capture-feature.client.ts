import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCaptureFeatureLegalRepresentativeSearchModel, MainProcessCaptureFeatureLegalRepresentativeSearchResultModel, MainProcessCaptureFeatureLegalRepresentativeSubmitModel, MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel, MainProcessCaptureFeatureOptionsModel, MainProcessCaptureFeatureOptionsResultModel, MainProcessCaptureFeaturePatientSubmitModel, MainProcessCaptureFeaturePatientSubmitResultModel } from "@app/api/main-process-capture-feature/main-process-capture-feature.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-capture";

@Injectable()
export class MainProcessCaptureFeatureClient {

  public async getOptions(model: MainProcessCaptureFeatureOptionsModel): Promise<MainProcessCaptureFeatureOptionsResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeatureOptionsResultModel>(
        `${baseAddress}/get-options`,
        model
      )
    );
  }

  public async searchLegalRepresentative(model: MainProcessCaptureFeatureLegalRepresentativeSearchModel): Promise<MainProcessCaptureFeatureLegalRepresentativeSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeatureLegalRepresentativeSearchResultModel>(
        `${baseAddress}/search-legal-representative`,
        model
      )
    );
  }

  public async submitPatient(model: MainProcessCaptureFeaturePatientSubmitModel): Promise<MainProcessCaptureFeaturePatientSubmitResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeaturePatientSubmitResultModel>(
        `${baseAddress}/submit-patient`,
        model
      )
    );
  }

  public async submitLegalRepresentative(model: MainProcessCaptureFeatureLegalRepresentativeSubmitModel): Promise<MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel>(
        `${baseAddress}/submit-legal-representative`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
