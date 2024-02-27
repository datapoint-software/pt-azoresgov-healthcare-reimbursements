import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { configure, init } from "./error.actions";
import { map } from "rxjs";
import { decodeBase64URIComponent, statusObjectFromCode } from "./error.helpers";

@Injectable()
export class ErrorEffects {

  constructor(
    private readonly actions$: Actions
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    map(({ payload }) => configure({
      payload: {
        id: payload.id,
        correlationId: payload.correlationId,
        status: statusObjectFromCode(payload.statusCode),
        message: decodeBase64URIComponent(payload.message),
        stackTrace: decodeBase64URIComponent(payload.stackTrace)
      }
    }))
  ));
}
