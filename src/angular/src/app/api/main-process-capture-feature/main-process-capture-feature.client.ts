import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCaptureFeatureOptionsModel, MainProcessCaptureFeatureOptionsResultModel } from "@app/api/main-process-capture-feature/main-process-capture-feature.abstractions";
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

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
