import { createReducer, on } from "@ngrx/store";
import { SignInState } from "./sign-in.state";
import { configure, dispose, init } from "./sign-in.actions";

export const reducer = createReducer(

  (undefined as unknown as SignInState),

  on(init, () => (undefined as unknown as SignInState)),
  on(dispose, () => (undefined as unknown as SignInState)),
  on(configure, (_, { payload }) => ({ ...payload }))
);
