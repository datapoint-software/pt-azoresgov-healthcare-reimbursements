import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { dequeue, dispose, enqueue, init } from "./rx/loading-overlay.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./loading-overlay.constants";
import { LoadingOverlayState } from "./rx/loading-overlay.state";
import { reducer } from "./rx/loading-overlay.reducer";
import { items, state } from "./rx/loading-overlay.selectors";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";

// TODO <joao.pl.lopes>
//
// Although very rare, this is one of those cases where a feature
// does not need the whole `init`, `dispose` and effects overhead.
//
// We need to find a cleaner way of implementing these sort of
// features.
@Injectable()
export class LoadingOverlayFeature extends Feature<LoadingOverlayState> {

  readonly items$ = this.of(items);

  constructor(store: Store) {
    super(store, state);
  }

  enqueue(id: string) {
    this.dispatch(enqueue({
      payload: {
        id
      }
    }));
  }

  dequeue(id: string) {
    this.dispatch(dequeue({
      payload: {
        id
      }
    }));
  }

  protected override dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    throw 'Feature `LoadingOverlay` can not be disposed.';
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    throw 'Feature `LoadingOverlay` can not be initialized.';
  }
}

export const provideLoadingOverlayFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    LoadingOverlayFeature
  ]),

  provideState(FEATURE_NAME, reducer)
];
