import { createReducer, on } from "@ngrx/store";
import { EnvironmentState } from "./environment.state";
import { configure, dispose } from "./environment.actions";

const initialState = undefined as unknown as EnvironmentState;

export const reducer = createReducer(

  initialState,

  on(configure, (_, action) => ({
    ...action.payload
  })),

  on(dispose, () => initialState)
);
