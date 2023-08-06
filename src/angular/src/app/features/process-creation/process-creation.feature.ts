import { Store } from "@ngrx/store";
import { StaticFeature } from "../feature.abstractions";
import { ProcessCreationState } from "./process-creation.state";
import { entitySelectionEnabled, state } from "./process-creation.selectors";
import { dispose, init, selectEntity, selectEntityComplete } from "./process-creation.actions";
import { Injectable } from "@angular/core";
import { ProcessCreationEffects } from "./process-creation.effects";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { filter, first, firstValueFrom, lastValueFrom, map, take } from "rxjs";

@Injectable()
export class ProcessCreationFeature extends StaticFeature<ProcessCreationState> {

  constructor(
    store: Store,
    private readonly actions$: Actions) {
    super(store, state, init, dispose);
  }

  readonly entitySelectionEnabled$ = this.createObservableOf(entitySelectionEnabled);

  async selectEntity(payload: {
    entityId: string
  }) {

    const completion$ = firstValueFrom(this.actions$.pipe(
      ofType(selectEntityComplete),
      take(1)
    ));

    this.store.dispatch(selectEntity({
      payload
    }));

    return await completion$;
  }
}
