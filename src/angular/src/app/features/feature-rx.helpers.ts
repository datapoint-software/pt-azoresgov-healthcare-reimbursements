import { HttpErrorResponse } from "@angular/common/http";
import { ObservableInput, Observable, OperatorFunction, ObservedValueOf, catchError, throwError } from "rxjs";
import { ErrorResponseModel } from "../clients/api.models";

export const catchStatus = <T, O extends ObservableInput<any>>(
  status: number,
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchError<T, O | Observable<never>>(
    (e, c) => (e instanceof HttpErrorResponse && e.status === status)
      ? selector(e.error, e, c)
      : throwError(() => e)
  );


export const catchConflict = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchStatus(409, selector);

export const catchForbidden = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchStatus(403, selector);

export const catchUnauthorized = <T, O extends ObservableInput<any>>(
  selector: (error: ErrorResponseModel, response: HttpErrorResponse, caught: Observable<unknown>) => O | Observable<never>
) : OperatorFunction<T, T | ObservedValueOf<O>> =>

  catchStatus(401, selector);
