import { HttpErrorResponse } from "@angular/common/http";
import { ErrorResponseModel } from "./api.abstractions";

export const conflict = <T>(
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<T> | T
): ((error: unknown) => Promise<T>) => async (error) => {

  if (error instanceof HttpErrorResponse && error.status === 409) {
    const r = fn(error.error, error);
    if (r) await r;
    return r;
  }

  throw error;
}
