import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { configure, dispose, init, writeConfiguration, writeConfigurationComplete, writePatient, writePatientComplete } from "./process-capture.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap } from "rxjs";
import { ProcessCaptureClient } from "../../../clients/process-capture/process-capture.client";
import { Store } from "@ngrx/store";

import * as $$ from "./process-capture.selectors";

@Injectable()
export class ProcessCaptureEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processCaptureClient: ProcessCaptureClient,
    private readonly store: Store
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(({ payload }) => this.processCaptureClient.getOptions(payload.processId).pipe(
      map((response) => configure({
        payload: {
          ...response,
          writting: false
        }
      }))
    ))
  ));

  readonly writeConfiguration$ = createEffect(() => this.actions$.pipe(
    ofType(writeConfiguration),
    concatLatestFrom(() => [
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeMap(([ { payload }, id, rowVersionId ]) => this.processCaptureClient.writeConfiguration(id, {
      rowVersionId,
      ...payload
    }).pipe(
      map((response) => writeConfigurationComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly writePatient$ = createEffect(() => this.actions$.pipe(
    ofType(writePatient),
    concatLatestFrom(() => [
      this.store.select($$.patientRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeMap(([ { payload }, patientRowVersionId, processId, processRowVersionId ]) => this.processCaptureClient.writePatient(processId, {
      patientRowVersionId,
      processRowVersionId,
      ...payload
    }).pipe(
      map((response) => writePatientComplete({
        payload: { ...response }
      }))
    ))
  ));
}
