import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { concatLatestFrom, ofType, provideEffects } from "@ngrx/effects";
import { dispose, init, next, previous, searchEntities, selectEntity } from "./rx/process-creation.actions";
import { entities, entitySearchResult, entitySearchResultEntityIds, entitySearchResultTotalMatchCount, state, step, steps } from "./rx/process-creation.selectors";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-creation.constants";
import { map } from "rxjs";
import { ProcessCreationEffects } from "./rx/process-creation.effects";
import { ProcessCreationState } from "./rx/process-creation.state";
import { reducer } from "./rx/process-creation.reducer";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";

@Injectable()
export class ProcessCreationFeature extends Feature<ProcessCreationState> {

  readonly entityById = (id: string) => this.createObservableFactory(entities)().pipe(
    map((entities) => entities[id])
  );

  readonly entitySearchResult$ = this.createObservableFactory(entitySearchResult);

  readonly entitySearchResultEmpty$ = () => this.createObservableFactory(entitySearchResultTotalMatchCount)().pipe(
    map((c) => c === 0)
  );

  readonly entitySearchResultMatches$ = () => this.createObservableFactory(entitySearchResultEntityIds)();

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

  selectEntity(id: string) {
    this.dispatch(selectEntity({
      payload: {
        id
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
