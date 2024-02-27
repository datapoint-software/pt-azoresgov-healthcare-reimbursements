import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { configure, init } from "./identity.actions";
import { map, mergeMap, of } from "rxjs";
import { IdentityClient } from "../../../clients/identity/identity.client";
import { catchConflict, catchForbidden, catchUnauthorized } from "../../feature-rx.helpers";

@Injectable()
export class IdentityEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly identityClient: IdentityClient
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.identityClient.getIdentity().pipe(
      map((response) => configure({
        payload: {
          ...response
        }
      })),
      catchConflict((response) => of(configure({ payload: { } }))),
      catchUnauthorized((response) => of(configure({ payload: { } })))
    ))
  ));

}
