import { createReducer, on } from "@ngrx/store";
import { SignInState } from "./sign-in.state";
import { dispose, initConfigure, signInPostError } from "./sign-in.actions";

const initialState = undefined as unknown as SignInState;

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(initConfigure, (_, action) => ({
    ...action.payload
  })),

  on(signInPostError, (state, action) => ({
    ...state,
    authentication: {
      ...state.authentication,
      error: action.payload
    }
  }))
);
