import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { APP_SEARCH_DELAY_MS } from "../../app.constants";
import { dispose, init, search } from "./rx/process-search.actions";
import { entities, searchResult, state } from "./rx/process-search.selectors";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./process-search.constants";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { ProcessSearchEffects } from "./rx/process-search.effects";
import { ProcessSearchState } from "./rx/process-search.state";
import { ProcessStatus } from "../../enums/process-status.enum";
import { provideEffects } from "@ngrx/effects";
import { reducer } from "./rx/process-search.reducer";
import { Store, provideState } from "@ngrx/store";
import { Subject, debounceTime, distinct, filter, takeUntil, tap } from "rxjs";

@Injectable()
export class ProcessSearchFeature extends Feature<ProcessSearchState> {

  private dispose$?: Subject<boolean>;

  readonly entities$ = this.of(entities);

  readonly searchResult$ = this.of(searchResult);

  readonly searchFormGroup = new FormGroup({
    entityId: new FormControl('', []),
    filter: new FormControl('', [ Validators.maxLength(128) ]),
    status: new FormControl(null as ProcessStatus | null, [ ])
  });

  constructor(store: Store) {
    super(store, state, dispose, init);
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

    this.dispose$ = new Subject<boolean>();

    this.searchFormGroup.valueChanges
      .pipe(takeUntil(this.dispose$))
      .pipe(filter(() => this.searchFormGroup.valid))
      .pipe(distinct())
      .pipe(debounceTime(APP_SEARCH_DELAY_MS))
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => {
        this.search();
      });

    await super.init(activatedRoute, router);
  }

  public override async dispose(): Promise<void> {

    if (this.dispose$) {

      this.dispose$.next(true);
      this.dispose$.complete();

      delete this.dispose$;
    }

    await super.dispose();
  }
}

export const provideProcessSearchFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ProcessSearchFeature
  ]),

  provideEffects(ProcessSearchEffects),
  provideState(FEATURE_NAME, reducer)
];
