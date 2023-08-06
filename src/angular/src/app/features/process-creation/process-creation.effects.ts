import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ProcessCreationClient } from "./process-creation.client";
import { init, initConfigure, selectEntity, selectEntityComplete } from "./process-creation.actions";
import { map, mergeMap } from "rxjs";
import { Injectable } from "@angular/core";

@Injectable()
export class ProcessCreationEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processCreationClient: ProcessCreationClient
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.processCreationClient.getOptions().pipe(
      map((response) => initConfigure({
        payload: response
      }))
    ))
  ));

  readonly selectEntity$ = createEffect(() => this.actions$.pipe(
    ofType(selectEntity),
    map(() => selectEntityComplete())
  ));
}
