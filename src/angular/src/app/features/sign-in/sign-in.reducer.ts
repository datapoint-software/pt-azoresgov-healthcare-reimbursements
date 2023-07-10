import { createReducer, on } from "@ngrx/store";
import { dispose, initConfigure, signInError } from "./sign-in.actions";
import { SignInState } from "./sign-in.state";

const initialState = (undefined as unknown as SignInState)!;

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(initConfigure, (_, action) => ({
    ...action.payload
  })),

  on(signInError, (state, action) => ({
    ...state,
    error: {
      ...action.payload
    }
  }))
);
