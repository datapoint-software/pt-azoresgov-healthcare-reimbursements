import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { configure, init } from "./error.actions";
import { filter, map } from "rxjs";
import { Injectable } from "@angular/core";
import { reset } from "../loading-overlay/loading-overlay.actions";
import { statusCodeMessages } from "./error.constants";
import { Store } from "@ngrx/store";
import { visible } from "../loading-overlay/loading-overlay.selectors";

@Injectable()
export class ErrorEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly store: Store
  ) {}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    map((action) => configure({
      payload: {
        id: action.payload.id,
        message: action.payload.message ? decodeURIComponent(atob(action.payload.message).replace(/\+/g, '%20')) : undefined,
        status: action.payload.statusCode && statusCodeMessages[action.payload.statusCode] ? {
          code: action.payload.statusCode,
          message: statusCodeMessages[action.payload.statusCode]
        } : undefined
      }
    }))
  ));

  public readonly resetLoadingOverlayOnInit$ = createEffect(() => this.init$.pipe(
    concatLatestFrom(() => this.store.select(visible)),
    map(([ _, visible ]) => visible),
    filter(visible => visible),
    map(() => reset())
  ));
}
