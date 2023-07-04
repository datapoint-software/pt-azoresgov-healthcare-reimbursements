import { Actions, createEffect, ofType } from "@ngrx/effects";
import { EMPTY, mergeMap, of, withLatestFrom } from "rxjs";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";

import * as actions from './loading-overlay.actions';
import * as selectors from './loading-overlay.selectors';

@Injectable()
export class LoadingOverlayEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly store: Store
  ) {}

  readonly dequeue$ = createEffect(() => this.actions$.pipe(
    ofType(actions.dequeue),
    mergeMap((action) => of(action).pipe(
      withLatestFrom(
        this.store.select(selectors.visible),
        this.store.select(selectors.tasksAsArray)
      ),
    )),
    mergeMap(([ _, visible, tasks ]) => {

      if ((!visible) || tasks.length > 0)
        return EMPTY;

      return of(actions.hide());
    })
  ));

  readonly enqueue$ = createEffect(() => this.actions$.pipe(
    ofType(actions.enqueue),
    mergeMap((action) => of(action).pipe(
      withLatestFrom(this.store.select(selectors.visible))
    )),
    mergeMap(([ action, visible ]) => {

      const next = actions.task({
        payload: {
          task: {
            id: action.payload.id,
            enqueued: new Date()
          }
        }
      });

      if (!visible)
        return of(next, actions.show());

      return of(next);
    })
  ));
}
