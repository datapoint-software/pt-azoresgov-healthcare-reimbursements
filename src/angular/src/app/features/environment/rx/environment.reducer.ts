import { createReducer, on } from "@ngrx/store";
import { EnvironmentState } from "./environment.state";
import { configure, dispose } from "./environment.actions";

export const reducer = createReducer(

  (undefined as unknown as EnvironmentState),

  on(dispose, () => (undefined as unknown as EnvironmentState)),

  on(configure, (_, { payload }) => ({
    ...payload
  }))
);
