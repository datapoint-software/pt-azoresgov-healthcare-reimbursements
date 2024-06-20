import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Injectable, NgZone } from "@angular/core";
import { Router } from "@angular/router";
import { ErrorResponseModel } from "@app/api/api.abstractions";
import { CoreErrorFeatureError } from "@app/features/core-error/core-error-feature.abstractions";
import { CORE_ERROR_FEATURE_STATUS_CODE_MESSAGES } from "@app/features/core-error/core-error-feature.constants";

@Injectable()
export class CoreErrorFeature {

  // #region State

  private _error: CoreErrorFeatureError | null = undefined!;

  // #endregion

  // #region State accessors

  public get error(): CoreErrorFeatureError | null {
    return this._error;
  }

  // #endregion

  // #region Actions

  public configure(
    id: string | null,
    correlationId: string | null,
    statusCode: number | null,
    message: string | null,
    stackTrace: string | null
  ): void {
    this._error = {
      id,
      correlationId,
      message,
      stackTrace,
      status: (statusCode && {
        code: statusCode,
        message: CORE_ERROR_FEATURE_STATUS_CODE_MESSAGES[statusCode] ?? "Unknown Error"
      }) || null
    };
  }

  public report(error: unknown): void {

    if (console.error)
      console.error(error);

    let id: string | null = null;
    let correlationId: string | null = null;
    let message: string | null = null;
    let stackTrace: string | null = null;
    let statusCode: number | null = null;

    if (error instanceof Error) {
      message = error.message;
      stackTrace = error.stack ?? null;
    }

    else if (error instanceof HttpErrorResponse) {

      statusCode = error.status;

      if (error.error?.source === "app") {

        const response = error.error as ErrorResponseModel;

        id = response.id ?? null;
        correlationId = response.correlationId ?? null;
        message = response.message;
      }
    }

    this._error = ({
      id,
      correlationId,
      message,
      stackTrace,
      status: (statusCode && ({
        code: statusCode,
        message: CORE_ERROR_FEATURE_STATUS_CODE_MESSAGES[statusCode] ?? "Unknown Status Code"
      }) || null)
    });

    this._ngZone.run(() => {

      if (!this._error)
        return;

      this._router.navigate([ '/error' ], {
        queryParams: {
          id: this._error.id,
          correlationId: this._error.correlationId,
          message: this._error.message,
          stackTrace: this._error.stackTrace,
          statusCode: this._error.status?.code ?? null
        }
      });
    })

  }

  // #endregion

  constructor(
    private readonly _ngZone: NgZone,
    private readonly _router: Router
  ) {}

}
