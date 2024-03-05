import { Injectable } from "@angular/core";
import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { configure, submit, init, next, previous, searchEntities, searchEntitiesComplete, searchPatients, searchPatientsComplete, selectEntity, selectPatient, step, submitComplete } from "./process-creation.actions";
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
    mergeMap(() => this.processCreationClient.getOptions().pipe(
      map((response) => configure({
        payload: {
          complete: false,
          step: 0,
          steps: [
            ...(response.entityId ? [] : [ 'entity' ]),
            'patient',
            'confirmation'
          ],
          entityId: response.entityId,
          entities: (response.entities || []).reduce(
            (pv, cv) => ({ ...pv, [cv.id]: { ...cv }}),
            {}
          ),
          patients: {}
        }
      }))
    ))
  ));

  readonly next$ = createEffect(() => this.actions$.pipe(
    ofType(next),
    concatLatestFrom(() => [
      this.store.select(selectors.steps),
      this.store.select(selectors.step)
    ]),
    map(([ _, steps, i ]) => (i === steps.length - 1) ? submit() : step({ payload: i + 1}))
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

  readonly searchPatients$ = createEffect(() => this.actions$.pipe(
    ofType(searchPatients),
    concatLatestFrom(() => this.store.select(selectors.entityId)),
    mergeLoadingOverlay(([ { payload }, entityId ]) => of(payload).pipe(
      mergeMap(({ filter }) => this.processCreationClient.searchPatients(entityId!, filter, 0, 5).pipe(
        map((payload) => searchPatientsComplete({ payload }))
      ))
    ))
  ));

  readonly selectEntity$ = createEffect(() => this.actions$.pipe(
    ofType(selectEntity),
    map(() => next())
  ));

  readonly selectPatient$ = createEffect(() => this.actions$.pipe(
    ofType(selectPatient),
    map(() => next())
  ));

  readonly submit$ = createEffect(() => this.actions$.pipe(
    ofType(submit),
    concatLatestFrom(() => [
      this.store.select(selectors.entities),
      this.store.select(selectors.patients),
      this.store.select(selectors.entityId),
      this.store.select(selectors.patientId)
    ]),
    mergeLoadingOverlay(([ _, entities, patients, entityId, patientId ]) => this.processCreationClient.create({
      entityId: entityId!,
      entityRowVersionId: entities[entityId!].rowVersionId,
      patientId: patientId!,
      patientRowVersionId: patients[patientId!].rowVersionId
    }).pipe(
      map((response) => submitComplete({
        payload: {
          id: response.id,
          rowVersionId: response.rowVersionId,
          number: response.number
        }
      }))
    ))
  ));
}
