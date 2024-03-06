import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { complete, process, entities, entityId, entitySearchResult, entitySearchResultEntityIds, entitySearchResultTotalMatchCount, patientId, patientSearchResult, patientSearchResultEntityIds, patientSearchResultTotalMatchCount, patients, processId, state, step, steps, entityName } from "./rx/process-creation.selectors";
import { concatLatestFrom, provideEffects } from "@ngrx/effects";
import { dispose, init, next, previous, redirectToProcessPatientCapture, searchEntities, searchPatients, selectEntity, selectPatient } from "./rx/process-creation.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-creation.constants";
import { map, mergeMap } from "rxjs";
import { ProcessCreationEffects } from "./rx/process-creation.effects";
import { ProcessCreationState } from "./rx/process-creation.state";
import { reducer } from "./rx/process-creation.reducer";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";

@Injectable()
export class ProcessCreationFeature extends Feature<ProcessCreationState> {

  readonly complete$ = this.of(complete);

  readonly entityById$ = (id: string) => this.of(entities).pipe(
    map((entities) => entities[id])
  );

  readonly entity$ = this.of(entityId).pipe(
    concatLatestFrom(() => this.of(entities)),
    map(([ id, entities ]) => entities[id!])
  );

  readonly entityId$ = this.of(entityId);

  readonly entityName$ = this.of(entityId).pipe(
    concatLatestFrom(() => [
      this.of(entities)
    ]),
    map(([ entityId, entities ]) => entities[entityId!].name)
  );

  readonly entitySearchResult$ = this.of(entitySearchResult);

  readonly entitySearchResultEmpty$ = this.of(entitySearchResultTotalMatchCount).pipe(
    map((c) => c === 0)
  );

  readonly entitySearchResultMatches$ = this.of(entitySearchResultEntityIds);

  readonly nextStepEnabled$ = this.of(step).pipe(
    concatLatestFrom(() => [
      this.of(steps),
      this.of(entityId),
      this.of(patientId)
    ]),
    map(([ step, steps, entityId, patientId ]) => (step < steps.length) && (
      (steps[step] === 'entity' && !!entityId) ||
      (steps[step] === 'patient' && !!patientId) ||
      (steps[step] === 'confirmation')
    ))
  );

  readonly patientById$ = (id: string) => this.of(patients).pipe(
    map((patients) => patients[id])
  );

  readonly patient$ = this.of(patientId).pipe(
    concatLatestFrom(() => this.of(patients)),
    map(([ id, patients ]) => patients[id!])
  );

  readonly patientId$ = this.of(patientId);

  readonly patientSearchResult$ = this.of(patientSearchResult);

  readonly patientSearchResultEmpty$ = this.of(patientSearchResultTotalMatchCount).pipe(
    map((c) => c === 0)
  );

  readonly patientSearchResultMatches$ = this.of(patientSearchResultEntityIds);

  readonly previousStepEnabled$ = this.of(step).pipe(
    map((step) => step > 0)
  );

  readonly process$ = this.of(process);

  readonly step$ = this.of(step);

  readonly stepCount$ = this.of(steps).pipe(
    map((steps) => steps.length)
  );

  readonly stepName$ = this.of(step).pipe(
    concatLatestFrom(() => this.store.select(steps)),
    map(([ step, steps ]) => steps[step])
  );

  readonly stepNumber$ = this.of(step).pipe(
    map((step) => step + 1)
  );

  constructor(store: Store) {
    super(store, state);
  }

  next() {
    this.dispatch(next());
  }

  previous() {
    this.dispatch(previous());
  }

  redirectToProcessPatientCapture() {
    this.dispatch(redirectToProcessPatientCapture());
  }

  searchEntities(filter?: string) {
    this.dispatch(searchEntities({
      payload: {
        filter
      }
    }));
  }

  searchPatients(filter?: string) {
    this.dispatch(searchPatients({
      payload: {
        filter
      }
    }));
  }

  selectEntity(id: string) {
    this.dispatch(selectEntity({
      payload: {
        id
      }
    }));
  }

  selectPatient(id: string) {
    this.dispatch(selectPatient({
      payload: {
        id
      }
    }));
  }

  protected override dispose$$$(): TypedAction<string> {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return init();
  }
}

export const provideProcessCreationFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessCreationFeature
  ]),

  provideEffects(ProcessCreationEffects),
  provideState(FEATURE_NAME, reducer)
];
