import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCaptureFeatureLegalRepresentativeRemoveModel, MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel, MainProcessCaptureFeatureLegalRepresentativeSelectModel, MainProcessCaptureFeatureLegalRepresentativeSelectResultModel, MainProcessCaptureFeatureLegalRepresentativeSubmitModel, MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel, MainProcessCaptureFeatureOptionsModel, MainProcessCaptureFeatureOptionsResultModel, MainProcessCaptureFeaturePatientSubmitModel, MainProcessCaptureFeaturePatientSubmitResultModel } from "@app/api/main-process-capture-feature/main-process-capture-feature.abstractions";
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

  public async removeLegalRepresentative(model: MainProcessCaptureFeatureLegalRepresentativeRemoveModel): Promise<MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel>(
        `${baseAddress}/remove-legal-representative`,
        model
      )
    );
  }

  public async selectLegalRepresentative(model: MainProcessCaptureFeatureLegalRepresentativeSelectModel): Promise<MainProcessCaptureFeatureLegalRepresentativeSelectResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCaptureFeatureLegalRepresentativeSelectResultModel>(
        `${baseAddress}/select-legal-representative`,
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
