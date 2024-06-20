import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { ErrorResponseModel } from "@app/api/api.abstractions";

const status = <T>(
  status: number,
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<T> | T
): ((error: unknown) => Promise<T>) => async (error) => {

  if (error instanceof HttpErrorResponse && error.status === status && error.error?.source === "app") {
    const r = fn(error.error, error);
    if (r) await r;
    return r;
  }

  throw error;
}

export const conflict = <T>(
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<T> | T
): ((error: unknown) => Promise<T>) =>

  status(HttpStatusCode.Conflict, fn);

export const forbidden = <T>(
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<T> | T
): ((error: unknown) => Promise<T>) =>

  status(HttpStatusCode.Forbidden, fn);

export const unauthorized = <T>(
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<T> | T
): ((error: unknown) => Promise<T>) =>

  status(HttpStatusCode.Unauthorized, fn);
