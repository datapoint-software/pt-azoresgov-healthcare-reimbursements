import { ActionCreator, Store } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";
import { Observable, Subject, filter, firstValueFrom, skipWhile } from "rxjs";

export abstract class Feature<TFeatureState extends object> {

  protected abstract readonly store: Store;

  protected abstract readonly state$: Observable<TFeatureState>;

}

export abstract class DisposableFeature<TFeatureState extends object> extends Feature<TFeatureState> {

  protected abstract readonly dispose$$: ActionCreator<string, () => TypedAction<string>>;

  protected abstract readonly init$$: ActionCreator<string, () => TypedAction<string>>;

  public readonly disposed$: Observable<boolean> = new Subject<boolean>();

  public readonly disposing$: Observable<boolean> = new Subject<boolean>();

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

}
