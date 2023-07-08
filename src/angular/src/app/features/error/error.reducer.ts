import { createReducer, on } from "@ngrx/store";
import { ErrorState } from "./error.state";
import { configure, dispose, init } from "./error.actions";

const initialState = (undefined as unknown as ErrorState);

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(configure, (_, action) => ({
    ...action.payload
  })),

  on(init, () => ({}))
);
