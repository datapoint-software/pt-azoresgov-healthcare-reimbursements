import { Feature } from "../feature.abstractions";
import { Injectable } from "@angular/core";
import { LoadingOverlayState } from "./loading-overlay.state";
import { reset } from "./loading-overlay.actions";
import { state, tasksAsArray, visible } from "./loading-overlay.selectors";
import { Store } from "@ngrx/store";

@Injectable()
export class LoadingOverlayFeature extends Feature<LoadingOverlayState> {

  constructor(store: Store) {
    super(store, state);
  }

  public readonly tasks$ = this.store.select(tasksAsArray);
  public readonly visible$ = this.store.select(visible);

  public reset() {
    this.store.dispatch(reset());
  }
}
