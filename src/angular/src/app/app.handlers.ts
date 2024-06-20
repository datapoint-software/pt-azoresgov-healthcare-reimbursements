import { ErrorHandler, Injectable } from "@angular/core";
import { CoreErrorFeature } from "@app/features/core-error/core-error.feature";

@Injectable()
export class AppErrorHandler implements ErrorHandler {

  handleError(error: any): void {
    this._errorFeature.report(error);
  }

  constructor(
    private readonly _errorFeature: CoreErrorFeature
  ) {}

}
