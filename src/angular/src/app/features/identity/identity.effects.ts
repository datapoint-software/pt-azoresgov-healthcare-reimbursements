import { Actions, createEffect, ofType } from "@ngrx/effects";
import { catchErrorResponseWithStatusCode } from "../../app.operators";
import { dequeue, enqueue } from "../loading-overlay/loading-overlay.actions";
import { IdentityClient } from "./identity.client";
import { init, initConfigure, initRefresh } from "./identity.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap, of } from "rxjs";

const loadingScreen = (id: string) => ({
  payload: {
    id
  }
});

@Injectable()
export class IdentityEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly identityClient: IdentityClient
  ) {}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => of(
      enqueue(loadingScreen(init.type)),
      initRefresh()
    ))
  ));

  public readonly initRefresh$ = createEffect(() => this.actions$.pipe(
    ofType(initRefresh),
    mergeMap(() => this.identityClient.refresh().pipe(
      map((claims) => initConfigure({ payload: { claims } })),
      catchErrorResponseWithStatusCode([ 401, 403, 409 ], () => of(
        initConfigure({ payload: {} })
      ))
    )),
    mergeMap((action) => [
      action,
      dequeue(loadingScreen(init.type))
    ])
  ));
}
