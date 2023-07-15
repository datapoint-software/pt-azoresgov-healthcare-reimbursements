import { HttpClient } from "@angular/common/http";
import { IdentityRefreshResultModel } from "./identity.models";
import { Injectable } from "@angular/core";

const baseAddress = '/api/features/identity';

@Injectable()
export class IdentityClient {

  constructor(
    private readonly httpClient: HttpClient
  ) {}

  public refresh() {
    return this.httpClient.post<IdentityRefreshResultModel>(
      `${baseAddress}/refresh`,
      undefined
    );
  }
}
