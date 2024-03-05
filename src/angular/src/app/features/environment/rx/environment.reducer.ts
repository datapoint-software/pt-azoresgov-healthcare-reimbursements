import { configure, dispose, init } from "./environment.actions";
import { createReducer, on } from "@ngrx/store";
import { EnvironmentState } from "./environment.state";

export const reducer = createReducer(

  (undefined as unknown as EnvironmentState),

  on(dispose, () => (undefined as unknown as EnvironmentState)),
  on(init, () => (undefined as unknown as EnvironmentState)),

  on(configure, (_, { payload }) => ({ ...payload }))
);
