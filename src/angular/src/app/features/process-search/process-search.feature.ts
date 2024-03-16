import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { provideEffects } from "@ngrx/effects";
import { Store, provideState } from "@ngrx/store";
import { debounceTime, distinct, filter, takeUntil } from "rxjs";
import { APP_SEARCH_DELAY_MS } from "../../app.constants";
import { ProcessStatus } from "../../enums/process-status.enum";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-search.constants";
import { dispose, init, search } from "./rx/process-search.actions";
import { ProcessSearchEffects } from "./rx/process-search.effects";
import { reducer } from "./rx/process-search.reducer";
import { entities, searchResult, state } from "./rx/process-search.selectors";
import { ProcessSearchState } from "./rx/process-search.state";

@Injectable()
export class ProcessSearchFeature extends Feature<ProcessSearchState> {

  readonly entities$ = this.of(entities);

  readonly searchResult$ = this.of(searchResult);

  readonly searchFormGroup = new FormGroup({
    entityId: new FormControl('', []),
    filter: new FormControl('', [ Validators.maxLength(128) ]),
    status: new FormControl(null as ProcessStatus | null, [ ])
  });

  constructor(store: Store) {
    super(store, state);
  }

  protected override dispose$$$() {
    return dispose();
  }

  protected override init$$$() {
    return init();
  }

  search() {
    this.dispatch(search({
      payload: {
        entityId: this.searchFormGroup.value.entityId || undefined,
        filter: this.searchFormGroup.value.filter || undefined,
        status: this.searchFormGroup.value.status || undefined
      }
    }));
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    await super.init(activatedRoute, router);

    this.searchFormGroup.setValue({
      entityId: null,
      filter: null,
      status: null
    });

    this.searchFormGroup.valueChanges
      .pipe(takeUntil(this.dispose$))
      .pipe(filter(() => this.searchFormGroup.valid))
      .pipe(distinct())
      .pipe(debounceTime(APP_SEARCH_DELAY_MS))
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => {
        this.search();
      });
  }
}

export const provideProcessSearchFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessSearchFeature
  ]),

  provideEffects(ProcessSearchEffects),
  provideState(FEATURE_NAME, reducer)
];
