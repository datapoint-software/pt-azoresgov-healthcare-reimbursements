import { createReducer, on } from "@ngrx/store";
import { dequeue, enqueue, reset } from "./loading-overlay.actions";
import { LoadingOverlayState } from "./loading-overlay.state";

const initialState: LoadingOverlayState = {
  tasks: {}
};

export const reducer = createReducer(

  initialState,

  on(dequeue, (state, action) => ({
    ...state,
    tasks: Object.values(state.tasks).reduce(
      (ns, t) => t.id === action.payload.id ? ns :
        ({ ...ns, [t.id]: { ...t }}),
      {}
    )
  })),

  on(enqueue, (state, action) => ({
    ...state,
    tasks: {
      ...state.tasks,
      [action.payload.id]: {
        ...action.payload,
        enqueued: new Date()
      }
    }
  })),

  on(reset, () => ({
    ...initialState
  }))
)
