import { configure, dispose } from "./identity.actions";
import { createReducer, on } from "@ngrx/store";
import { IdentityState } from "./identity.state";

export const reducer = createReducer(

  (undefined as unknown as IdentityState),

  on(dispose, () => (undefined as unknown as IdentityState)),

  on(configure, (_, { payload }) => ({
    ...payload
  }))
);
