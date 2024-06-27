import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCaptureFeatureLegalRepresentativeSearchModel, MainProcessCaptureFeatureLegalRepresentativeSearchResultModel, MainProcessCaptureFeatureOptionsModel, MainProcessCaptureFeatureOptionsResultModel, MainProcessCaptureFeaturePatientSubmitModel, MainProcessCaptureFeaturePatientSubmitResultModel } from "@app/api/main-process-capture-feature/main-process-capture-feature.abstractions";
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
        `${baseAddress}/update-patient`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
