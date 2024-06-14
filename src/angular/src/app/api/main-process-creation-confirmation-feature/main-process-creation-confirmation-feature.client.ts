import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { MainProcessCreationConfirmationFeatureConfirmModel, MainProcessCreationConfirmationFeatureConfirmResultModel } from "@app/api/main-process-creation-confirmation-feature/main-process-creation-confirmation-feature-client.abstractions";
import { firstValueFrom } from "rxjs";

const baseAddress = "/api/features/main-process-creation-confirmation";

@Injectable()
export class MainProcessCreationConfirmationFeatureClient {

  public async confirm(model: MainProcessCreationConfirmationFeatureConfirmModel): Promise<MainProcessCreationConfirmationFeatureConfirmResultModel> {
    return await firstValueFrom(
      this._httpClient.post<MainProcessCreationConfirmationFeatureConfirmResultModel>(
        `${baseAddress}/confirm`,
        model
      )
    );
  }

  constructor(
    private readonly _httpClient: HttpClient
  ) {}

}
