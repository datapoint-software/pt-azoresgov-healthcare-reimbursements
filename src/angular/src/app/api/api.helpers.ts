import { HttpErrorResponse } from "@angular/common/http";
import { ErrorResponseModel } from "./api.abstractions";

export const conflict = (
  fn: (model: ErrorResponseModel, response: HttpErrorResponse) => Promise<void> | void
): ((error: unknown) => Promise<undefined>) => async (error) => {

  if (error instanceof HttpErrorResponse && error.status === 409) {
    const r = fn(error.error, error);
    if (r) await r;
    return undefined;
  }

  throw error;
}
