import { createReducer, on } from "@ngrx/store";
import { ProcessCreationState } from "./process-creation.state";
import { dispose, initConfigure, selectEntity } from "./process-creation.actions";

const initialState = undefined as unknown as ProcessCreationState;

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(initConfigure, (_, action) => ({
    ...action.payload
  })),

  on(selectEntity, (state, action) => ({
    ...state,
    ...action.payload
  }))
);
