import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { configure, init } from "./environment.actions";
import { map, mergeMap } from "rxjs";
import { EnvironmentClient } from "../../../clients/environment/environment.client";

@Injectable()
export class EnvironmentEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly environment: EnvironmentClient
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.environment.getEnvironment().pipe(
      map((response) => configure({
        payload: {
          ...response
        }
      }))
    ))
  ));
}
