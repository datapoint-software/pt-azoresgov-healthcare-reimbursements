import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { DefaultProjectorFn, MemoizedSelector, Store } from "@ngrx/store";
import { Observable, Subject, firstValueFrom, map, skipWhile, takeUntil } from "rxjs";
import { TypedAction } from "@ngrx/store/src/models";

export abstract class Feature<TState> {

  protected abstract dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string>;

  protected abstract init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string>;

  protected readonly state$ = this.store.select(this.state$$);

  private dispose$?: Observable<boolean>;

  constructor(
    protected readonly store: Store,
    private readonly state$$: MemoizedSelector<object, TState, DefaultProjectorFn<TState>>
  ) { }

  protected createObservableFactory<TParentState, TValue>(selector: MemoizedSelector<object, TValue, (s1: TParentState) => TValue>) {

    return () => {

      if (!this.dispose$)
        throw 'Feature state is not ready for observing.';

      return this.store
        .select(selector)
        .pipe(takeUntil(this.dispose$));
    };
  }

  protected dispatch(action: TypedAction<string>) {
    this.store.dispatch(action);
  }

  public async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    this.dispose$ = new Subject<boolean>();

    this.store.dispatch(
      this.init$$$(activatedRoute, router)
    );

    await firstValueFrom(
      this.state$
        .pipe(map(state => !state))
        .pipe(skipWhile(preparing => preparing))
    );
  }

  public async dispose(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    if (this.dispose$) {
      (this.dispose$ as Subject<boolean>).next(true);
      (this.dispose$ as Subject<boolean>).complete();
      delete this.dispose$;
    }

    this.store.dispatch(
      this.dispose$$$(activatedRoute, router)
    );

    await firstValueFrom(
      this.state$
        .pipe(map(state => !!state))
        .pipe(skipWhile(disposing => disposing))
    );
  }
}
