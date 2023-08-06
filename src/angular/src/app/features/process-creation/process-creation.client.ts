import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ProcessCreationOptionsResultModel } from "./process-creation.models";

const baseAddress = '/api/features/process-creation';

@Injectable()
export class ProcessCreationClient {

  constructor(
    private readonly httpClient: HttpClient
  ) {}

  getOptions() {
    return this.httpClient.get<ProcessCreationOptionsResultModel>(
      baseAddress
    );
  }
}
