import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { DefaultProjectorFn, MemoizedSelector, Store } from "@ngrx/store";
import { firstValueFrom, map, skipWhile } from "rxjs";
import { TypedAction } from "@ngrx/store/src/models";

export abstract class Feature<TState> {

  protected readonly state$ = this.store.select(this.state$$);

  constructor(
    protected readonly store: Store,
    protected readonly state$$: MemoizedSelector<object, TState, DefaultProjectorFn<TState>>,
    protected readonly dispose$$$?: (() => TypedAction<string>) | TypedAction<string>,
    protected readonly init$$$?: ((activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) => TypedAction<string>) | TypedAction<string>
  ) { }

  protected dispatch(action: TypedAction<string>) {
    this.store.dispatch(action);
  }

  protected of<TParentState, TValue>(selector: MemoizedSelector<object, TValue, (s1: TParentState) => TValue>) {
    return this.store.select(selector);
  }

  public async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    if (!this.init$$$)
      return;

    const init$$$ = this.init$$$ instanceof Function
      ? this.init$$$(activatedRoute, router)
      : this.init$$$;

    this.dispatch(init$$$);

    await firstValueFrom(
      this.state$
        .pipe(map(state => !state))
        .pipe(skipWhile(preparing => preparing))
    );
  }

  public async dispose(): Promise<void> {

    if (!this.dispose$$$)
      return;

    const dispose$$$ = this.dispose$$$ instanceof Function
      ? this.dispose$$$()
      : this.dispose$$$;

    this.dispatch(dispose$$$);

    await firstValueFrom(
      this.state$
        .pipe(map(state => !!state))
        .pipe(skipWhile(disposing => disposing))
    );
  }
}
