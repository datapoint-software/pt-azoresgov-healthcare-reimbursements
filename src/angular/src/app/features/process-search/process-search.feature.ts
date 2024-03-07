import { dispose, init, search } from "./rx/process-search.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-search.constants";
import { ProcessSearchEffects } from "./rx/process-search.effects";
import { ProcessSearchState } from "./rx/process-search.state";
import { provideEffects } from "@ngrx/effects";
import { reducer } from "./rx/process-search.reducer";
import { entities, searchResult, state } from "./rx/process-search.selectors";
import { Store, provideState } from "@ngrx/store";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Injectable()
export class ProcessSearchFeature extends Feature<ProcessSearchState> {

  readonly entities$ = this.of(entities);

  readonly searchResult$ = this.of(searchResult);

  readonly searchFormGroup = new FormGroup({
    filter: new FormControl('', [ Validators.maxLength(128) ]),
    entityId: new FormControl('', [])
  })

  constructor(store: Store) {
    super(store, state, dispose, init);
  }

  search() {
    this.dispatch(search({
      payload: {
        entityId: this.searchFormGroup.value.entityId || undefined,
        filter: this.searchFormGroup.value.filter || undefined,
        skip: 0,
        take: 25
      }
    }));
  }
}

export const provideProcessSearchFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessSearchFeature
  ]),

  provideEffects(ProcessSearchEffects),
  provideState(FEATURE_NAME, reducer)
];
