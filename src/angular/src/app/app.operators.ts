import { ErrorResponseModel } from "./app.models";
import { HttpErrorResponse, HttpStatusCode } from "@angular/common/http";
import { Observable, ObservableInput, ObservedValueOf, OperatorFunction, catchError, throwError } from "rxjs";

export const catchErrorResponse = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> => {

  return catchError<T, O | Observable<never>>((e, c) =>
    (e instanceof HttpErrorResponse && e.error.source === 'app')
      ? selector(e.error, e, c)
      : throwError(() => e)
  );
}

export const catchErrorResponseWithStatusCode = <T, O extends ObservableInput<any>>(
  statusCodes: number[],
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> => {
  return catchErrorResponse((error, response, caught) =>
    statusCodes.indexOf(response.status) > -1
      ? selector(error, response, caught)
      : throwError(() => error)
  );
};

export const catchErrorResponseWithBadRequestOrConflictStatusCode = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchErrorResponseWithStatusCode([ HttpStatusCode.BadRequest, HttpStatusCode.Conflict ], selector);

export const catchErrorResponseWithBadRequestStatusCode = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchErrorResponseWithStatusCode([ HttpStatusCode.BadRequest ], selector);

export const catchErrorResponseWithConflictStatusCode = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchErrorResponseWithStatusCode([ HttpStatusCode.Conflict ], selector);

export const catchErrorResponseWithUnauthorizedStatusCode = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchErrorResponseWithStatusCode([ HttpStatusCode.Unauthorized ], selector);
