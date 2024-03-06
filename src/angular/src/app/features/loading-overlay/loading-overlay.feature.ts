import { dequeue, enqueue } from "./rx/loading-overlay.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./loading-overlay.constants";
import { LoadingOverlayState } from "./rx/loading-overlay.state";
import { reducer } from "./rx/loading-overlay.reducer";
import { items, state } from "./rx/loading-overlay.selectors";
import { Store, provideState } from "@ngrx/store";

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
}

export const provideLoadingOverlayFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    LoadingOverlayFeature
  ]),

  provideState(FEATURE_NAME, reducer)
];
