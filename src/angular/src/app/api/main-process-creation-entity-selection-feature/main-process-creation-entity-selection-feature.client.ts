import { firstValueFrom } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCreationEntitySelectionSearchModel, MainProcessCreationEntitySelectionSearchResultModel } from "@app/api/main-process-creation-entity-selection-feature/main-process-creation-entity-selection-feature-client.abstractions";

const baseAddress = "/api/features/main-process-creation-entity-selection";

@Injectable()
export class MainProcessCreationEntitySelectionFeatureClient {

  public async search(model: MainProcessCreationEntitySelectionSearchModel): Promise<MainProcessCreationEntitySelectionSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationEntitySelectionSearchResultModel>(
        `${baseAddress}/search`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
