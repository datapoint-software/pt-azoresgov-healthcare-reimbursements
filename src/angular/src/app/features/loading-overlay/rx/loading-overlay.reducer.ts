import { createReducer, on } from "@ngrx/store";
import { LoadingOverlayState } from "./loading-overlay.state";
import { dequeue, enqueue } from "./loading-overlay.actions";

export const reducer = createReducer(

  ({ items: {} } as LoadingOverlayState),

  on(enqueue, (state, { payload }) => ({
    ...state,
    items: {
      [ payload.id ]: {
        enqueuement: new Date()
      }
    }
  })),

  on(dequeue, (state, { payload }) => {
    let ns = { ...state, items: { ...state.items } };
    delete ns.items[payload.id];
    return ns;
  })
);
