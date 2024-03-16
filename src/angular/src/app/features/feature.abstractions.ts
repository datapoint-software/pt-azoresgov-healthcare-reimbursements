import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { DefaultProjectorFn, MemoizedSelector, Store } from "@ngrx/store";
import { Observable, Subject, firstValueFrom, map, skipWhile } from "rxjs";
import { TypedAction } from "@ngrx/store/src/models";

export abstract class Manager<TState> {

  private disposeSubject$?: Subject<boolean>;

  protected readonly state$ = this.store.select(this.state$$);

  constructor(
    protected readonly store: Store,
    protected readonly state$$: MemoizedSelector<object, TState, DefaultProjectorFn<TState>>,
    protected readonly managers?: Array<Manager<TState>>
  ) { }

  protected get dispose$() {

    if (!this.disposeSubject$)
      throw new Error('Operation out of sync.');

    return this.disposeSubject$ as Observable<boolean>;
  }

  protected dispatch(action: TypedAction<string>) {
    this.store.dispatch(action);
  }

  protected of<TOutput>(selector: ((a0: object) => TOutput)) {
    return this.store.select(selector);
  }

  public async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    this.disposeSubject$ = new Subject<boolean>();

    for (let manager of this.managers ?? [])
      await manager.init(activatedRoute, router);
  }

  public async dispose(): Promise<void> {

    if (this.disposeSubject$) {

      this.disposeSubject$.next(true);
      this.disposeSubject$.complete();

      delete this.disposeSubject$;
    }

    for (let manager of this.managers ?? [])
      await manager.dispose();

  }
}

export abstract class Feature<TState> extends Manager<TState> {

  constructor(
    store: Store,
    state$$: MemoizedSelector<object, TState, DefaultProjectorFn<TState>>,
    managers?: Array<Manager<TState>>
  ) {
    super(store, state$$, managers);
  }

  protected dispose$$$(): TypedAction<string> | null {
    return null;
  }

  protected init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> | null {
    return null;
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): Promise<void> {

    await super.init(activatedRoute, router);

    const init$$$ = this.init$$$(activatedRoute, router);

    if (!init$$$)
      return;

    this.dispatch(init$$$);

    await firstValueFrom(
      this.state$
        .pipe(map(state => !state))
        .pipe(skipWhile(preparing => preparing))
    );
  }

  public override async dispose(): Promise<void> {

    await super.dispose();

    const dispose$$$ = this.dispose$$$();

    if (!dispose$$$)
      return;

    this.dispatch(dispose$$$);

    await firstValueFrom(
      this.state$
        .pipe(map(state => !!state))
        .pipe(skipWhile(disposing => disposing))
    );
  }
}
