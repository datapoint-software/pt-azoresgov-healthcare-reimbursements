import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { dispose, init, next, previous, searchEntities, selectEntity } from "./rx/process-creation.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-creation.constants";
import { ProcessCreationEffects } from "./rx/process-creation.effects";
import { ProcessCreationState } from "./rx/process-creation.state";
import { concatLatestFrom, ofType, provideEffects } from "@ngrx/effects";
import { reducer } from "./rx/process-creation.reducer";
import { entitySearchResult, entitySearchResultEntities, entitySearchResultMatches, entitySearchResultTotalMatchCount, state, step, steps } from "./rx/process-creation.selectors";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";
import { map, withLatestFrom } from "rxjs";

@Injectable()
export class ProcessCreationFeature extends Feature<ProcessCreationState> {

  readonly entitySearchResult$ = this.createObservableFactory(entitySearchResult);

  readonly entitySearchResultEmpty$ = () => this.createObservableFactory(entitySearchResultTotalMatchCount)().pipe(
    map((c) => c === 0)
  );

  readonly entitySearchResultEntityById$ = (id: string) => this.createObservableFactory(entitySearchResultEntities)().pipe(
    map((entities) => entities.find(e => e.id === id))
  );

  readonly entitySearchResultMatches$ = () => this.createObservableFactory(entitySearchResultMatches)();

  readonly nextStepEnabled$ = () => this.createObservableFactory(step)().pipe(
    concatLatestFrom(() => this.store.select(steps)),
    map(([ step, steps ]) => (step + 1) < steps.length)
  );

  readonly previousStepEnabled$ = () => this.createObservableFactory(step)().pipe(
    map((step) => step > 0)
  );

  readonly step$ = this.createObservableFactory(step);

  readonly stepCount$ = () => this.createObservableFactory(steps)().pipe(
    map((steps) => steps.length)
  );

  readonly stepName$ = () => this.createObservableFactory(step)().pipe(
    concatLatestFrom(() => this.store.select(steps)),
    map(([ step, steps ]) => steps[step])
  );

  readonly stepNumber$ = () => this.createObservableFactory(step)().pipe(
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

  searchEntities(filter?: string) {
    this.dispatch(searchEntities({
      payload: {
        filter
      }
    }));
  }

  selectEntity(id: string, rowVersionId: string) {
    this.dispatch(selectEntity({
      payload: {
        id,
        rowVersionId
      }
    }));
  }

  protected override dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
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
