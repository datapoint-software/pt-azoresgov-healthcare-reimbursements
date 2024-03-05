import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { ProcessPatientCaptureClient } from "../../../clients/process-patient-capture/process-patient-capture.client";
import { configure, init } from "./process-patient-capture.actions";
import { map, mergeMap } from "rxjs";

@Injectable()
export class ProcessPatientCaptureEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processPatientCaptureClient: ProcessPatientCaptureClient
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(({ payload }) => this.processPatientCaptureClient.getOptions(payload.processId).pipe(
      map(({ entity, parentEntity, patient, process }) => configure({
        payload: {
          entity,
          parentEntity,
          patient,
          process
        }
      }))
    ))
  ));
}
