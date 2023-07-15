import { ActionCreator, DefaultProjectorFn, MemoizedSelector, Store } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";
import { Observable, Subject, filter, firstValueFrom, skipWhile, takeUntil } from "rxjs";

export abstract class Feature<TFeatureState extends object> {

  constructor(
    protected readonly store: Store,
    private readonly state: MemoizedSelector<object, TFeatureState, DefaultProjectorFn<TFeatureState>>
  ) {

  }

  protected readonly state$: Observable<TFeatureState> = this.store.select(this.state);
}

export abstract class DynamicFeature<TFeatureState extends object, TInitPayload> extends Feature<TFeatureState> {

  public readonly disposed$: Observable<boolean> = new Subject<boolean>();

  public readonly disposing$: Observable<boolean> = new Subject<boolean>();

  constructor(
    store: Store,
    state: MemoizedSelector<object, TFeatureState, DefaultProjectorFn<TFeatureState>>,
    private readonly init$$: ActionCreator<string, (props: { payload: TInitPayload }) => TypedAction<string> & { payload: TInitPayload }>,
    private readonly dispose$$: ActionCreator<string, () => TypedAction<string>>
  ) {
    super(store, state);
  }

  public async init(payload: TInitPayload): Promise<void> {

    const state = await firstValueFrom(this.state$);

    if (state) {
      console.warn(`Action '${this.init$$.type}' will not dispatch because the feature state is already set.`);
      return;
    }

    this.store.dispatch(this.init$$({ payload }));

    await firstValueFrom(this.state$.pipe(
      skipWhile((state) => state === undefined)
    ));
  }

  public async dispose(): Promise<void> {

    const state = await firstValueFrom(this.state$);

    if (state) {

      (this.disposing$ as Subject<boolean>).next(true);

      this.store.dispatch(this.dispose$$());

      await firstValueFrom(this.state$.pipe(
        filter((state) => state === undefined)
      ));

      (this.disposed$ as Subject<boolean>).next(true);
    }
  }

  protected createObservableOf<TState>(mapFn: (value: object) => TState) {
    return this.store.select(mapFn).pipe(takeUntil(this.disposing$));
  }
}

export abstract class StaticFeature<TFeatureState extends object> extends Feature<TFeatureState> {

  public readonly disposed$: Observable<boolean> = new Subject<boolean>();

  public readonly disposing$: Observable<boolean> = new Subject<boolean>();

  constructor(
    store: Store,
    state: MemoizedSelector<object, TFeatureState, DefaultProjectorFn<TFeatureState>>,
    private readonly init$$: ActionCreator<string, () => TypedAction<string>>,
    private readonly dispose$$: ActionCreator<string, () => TypedAction<string>>
  ) {
    super(store, state);
  }

  public async init(): Promise<void> {

    const state = await firstValueFrom(this.state$);

    if (state) {
      console.warn(`Action '${this.init$$.type}' will not dispatch because the feature state is already set.`);
      return;
    }

    this.store.dispatch(this.init$$());

    await firstValueFrom(this.state$.pipe(
      skipWhile((state) => state === undefined)
    ));
  }

  public async dispose(): Promise<void> {

    const state = await firstValueFrom(this.state$);

    if (state) {

      (this.disposing$ as Subject<boolean>).next(true);

      this.store.dispatch(this.dispose$$());

      await firstValueFrom(this.state$.pipe(
        filter((state) => state === undefined)
      ));

      (this.disposed$ as Subject<boolean>).next(true);
    }
  }

  protected createObservableOf<TState>(mapFn: (value: object) => TState) {
    return this.store.select(mapFn).pipe(takeUntil(this.disposing$));
  }
}
