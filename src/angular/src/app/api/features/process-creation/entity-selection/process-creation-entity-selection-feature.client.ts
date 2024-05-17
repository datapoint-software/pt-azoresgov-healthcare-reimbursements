import { firstValueFrom } from "rxjs";
import { ProcessCreationEntitySelectionSearchModel, ProcessCreationEntitySelectionSearchResultModel } from "./process-creation-entity-selection-feature-client.abstractions";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

const baseAddress = "/api/features/process-creation/entity-selection";

@Injectable()
export class ProcessCreationEntitySelectionFeatureClient {

  public async search(model: ProcessCreationEntitySelectionSearchModel): Promise<ProcessCreationEntitySelectionSearchResultModel> {
    return await firstValueFrom(
      this._httpClient.post<ProcessCreationEntitySelectionSearchResultModel>(
        `${baseAddress}/search`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}
}
