import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { IdentityRefreshModel, IdentityRefreshResultModel } from "./identity.models";
import { Observable } from "rxjs";

const baseAddress = '/api/features/identity';

const createRequestOptions = (accessToken: string) => ({
  headers: {
    "Authorization": `Bearer ${accessToken}`
  }
});

@Injectable()
export class IdentityClient {

  constructor(private readonly httpClient: HttpClient) {

  }

  public refresh(accessToken: string, model: IdentityRefreshModel): Observable<IdentityRefreshResultModel> {
    return this.httpClient.post<IdentityRefreshResultModel>(
      `${baseAddress}/refresh`,
      model,
      createRequestOptions(accessToken)
    );
  }
}
