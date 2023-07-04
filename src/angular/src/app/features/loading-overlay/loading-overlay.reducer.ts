import { createReducer, on } from "@ngrx/store";
import { LoadingOverlayState } from "./loading-overlay.state";

import * as actions from './loading-overlay.actions';

const initialState: LoadingOverlayState = {
  tasks: {},
  visible: false
};

export const reducer = createReducer(

  initialState,

  on(actions.dequeue, (state, action) => ({
    ...state,
    tasks: Object.values(state.tasks).reduce(
      (ns, t) => t.id === action.payload.id ? ns :
        ({ ...ns, [t.id]: { ...t }}),
      {}
    )
  })),

  on(actions.hide, (state) => ({
    ...state,
    visible: false
  })),

  on(actions.reset, () => ({
    ...initialState
  })),

  on(actions.show, (state) => ({
    ...state,
    visible: true
  })),

  on(actions.task, (state, action) => ({
    ...state,
    tasks: {
      ...state.tasks,
      [action.payload.task.id]: {
        ...action.payload.task
      }
    }
  }))
)
