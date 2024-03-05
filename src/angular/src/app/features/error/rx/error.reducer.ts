import { createReducer, on } from "@ngrx/store";
import { ErrorState } from "./error.state";
import { configure, dispose, init } from "./error.actions";

export const reducer = createReducer(

  (undefined as unknown as ErrorState),

  on(dispose, () => (undefined as unknown as ErrorState)),
  on(init, () => (undefined as unknown as ErrorState)),

  on(configure, (_, { payload }) => ({ ...payload }))
);
