import { Injectable } from "@angular/core";
import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { configure, init, next, previous, searchEntities, searchEntitiesComplete, selectEntity, step } from "./process-creation.actions";
import { map, mergeMap, of } from "rxjs";
import { Store } from "@ngrx/store";

import * as selectors from './process-creation.selectors';
import { mergeLoadingOverlay } from "../../loading-overlay/rx/loading-overlay.operators";
import { ProcessCreationClient } from "../../../clients/process-creation/process-creation.client";

@Injectable()
export class ProcessCreationEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processCreationClient: ProcessCreationClient,
    private readonly store: Store
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    map(() => configure({
      payload: {
        step: 0,
        steps: [ 'entity', 'patient', 'confirmation' ]
      }
    }))
  ));

  readonly next$ = createEffect(() => this.actions$.pipe(
    ofType(next),
    concatLatestFrom(() => this.store.select(selectors.step)),
    map(([ _, i ]) => step({ payload: i + 1}))
  ));

  readonly previous$ = createEffect(() => this.actions$.pipe(
    ofType(previous),
    concatLatestFrom(() => this.store.select(selectors.step)),
    map(([ _, i ]) => step({ payload: i - 1}))
  ));

  readonly searchEntities$ = createEffect(() => this.actions$.pipe(
    ofType(searchEntities),
    mergeLoadingOverlay(({ payload }) => of(payload).pipe(
      mergeMap(({ filter }) => this.processCreationClient.searchEntities(filter, 0, 5).pipe(
        map((payload) => searchEntitiesComplete({ payload }))
      ))
    ))
  ));

  readonly selectEntity$ = createEffect(() => this.actions$.pipe(
    ofType(selectEntity),
    map(() => next())
  ));
}
