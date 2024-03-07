import { Injectable } from "@angular/core";
import { ProcessSearchClient } from "../../../clients/process-search/process-search.client";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { configure, init, search, searchComplete } from "./process-search.actions";
import { map, mergeMap } from "rxjs";

@Injectable()
export class ProcessSearchEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processSearchClient: ProcessSearchClient
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.processSearchClient.getOptions().pipe(
      mergeMap((response) => [
        configure({
          payload: { ...response }
        }),
        search({
          payload: {}
        })
      ])
    ))
  ));

  readonly search$ = createEffect(() => this.actions$.pipe(
    ofType(search),
    mergeMap(({ payload }) => this.processSearchClient.search(
      payload.entityId,
      payload.filter,
      payload.status
    ).pipe(
      map((response) => searchComplete({
        payload: response
      }))
    ))
  ));
}
