import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCreationPatientSelectionFeatureSearchModel, MainProcessCreationPatientSelectionFeatureSearchResultModel } from "@app/api/main-process-creation-patient-selection-feature/main-process-creation-patient-selection-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-creation-patient-selection";

@Injectable()
export class MainProcessCreationPatientSelectionFeatureClient {

  constructor(
    private readonly _httpClient: HttpClient
  ) {}

  public async search(model: MainProcessCreationPatientSelectionFeatureSearchModel): Promise<MainProcessCreationPatientSelectionFeatureSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationPatientSelectionFeatureSearchResultModel>(
        `${baseAddress}/search`,
        model
      )
    );
  }
}
