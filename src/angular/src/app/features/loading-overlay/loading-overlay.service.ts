import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Feature } from "../feature.abstractions";
import { LoadingOverlayState } from "./loading-overlay.state";

import * as actions from './loading-overlay.actions';
import * as selectors from './loading-overlay.selectors';

@Injectable()
export class LoadingOverlayService extends Feature<LoadingOverlayState> {

  constructor(protected readonly store: Store) {
    super();
  }

  public readonly dequeue$$ = actions.dequeue;
  public readonly enqueue$$ = actions.enqueue;
  public readonly hide$$ = actions.hide;
  public readonly reset$$ = actions.reset;
  public readonly show$$ = actions.show;
  public readonly task$$ = actions.task;

  public readonly state$ = this.store.select(selectors.state);
  public readonly tasks$ = this.store.select(selectors.tasksAsArray);
  public readonly visible$ = this.store.select(selectors.visible);

}
