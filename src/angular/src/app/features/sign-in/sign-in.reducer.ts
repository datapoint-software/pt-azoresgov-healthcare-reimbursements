import { createReducer, on } from "@ngrx/store";
import { dispose, init, submit } from "./sign-in.actions";
import { SignInState } from "./sign-in.state";

const initialState = (undefined as unknown as SignInState)!;

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(init.configure, (_, action) => ({
    ...action.payload
  })),

  on(submit.error, (state, action) => ({
    ...state,
    error: {
      ...action.payload
    }
  }))
);
