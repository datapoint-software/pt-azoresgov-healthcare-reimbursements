import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { EnvironmentResultModel } from "./environment.models";

const baseAddress = '/api/environment';

@Injectable()
export class EnvironmentClient {

  constructor(
    private readonly httpClient: HttpClient
  ) {}

  public getEnvironment() {
    return this.httpClient.get<EnvironmentResultModel>(
      baseAddress
    );
  }
}
