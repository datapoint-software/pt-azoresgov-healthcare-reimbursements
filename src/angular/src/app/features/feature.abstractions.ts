import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { DefaultProjectorFn, MemoizedSelector, Store } from "@ngrx/store";
import { firstValueFrom, map, skipWhile } from "rxjs";
import { TypedAction } from "@ngrx/store/src/models";

export abstract class Feature<TState> {

  protected abstract dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string>;

  protected abstract init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string>;

  protected readonly state$ = this.store.select(this.state$$);

  constructor(
    protected readonly store: Store,
    private readonly state$$: MemoizedSelector<object, TState, DefaultProjectorFn<TState>>
  ) { }

  protected of<TParentState, TValue>(selector: MemoizedSelector<object, TValue, (s1: TParentState) => TValue>) {
    return this.store.select(selector);
  }

  protected dispatch(action: TypedAction<string>) {
    this.store.dispatch(action);
  }

  public async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    this.dispatch(
      this.init$$$(activatedRoute, router)
    );

    await firstValueFrom(
      this.state$
        .pipe(map(state => !state))
        .pipe(skipWhile(preparing => preparing))
    );
  }

  public async dispose(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    this.dispatch(
      this.dispose$$$(activatedRoute, router)
    );

    await firstValueFrom(
      this.state$
        .pipe(map(state => !!state))
        .pipe(skipWhile(disposing => disposing))
    );
  }
}
