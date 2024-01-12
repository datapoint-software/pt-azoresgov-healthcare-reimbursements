import { Injectable } from "@angular/core";
import { EnvironmentClient } from "./environment.client";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { configure, init } from "./environment.actions";
import { map, mergeMap } from "rxjs";

@Injectable()
export class EnvironmentEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly environmentClient: EnvironmentClient
  ) {}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.environmentClient.getEnvironment().pipe(
      map((result) => configure({
        payload: {
          ...result
        }
      }))
    ))
  ));
}
