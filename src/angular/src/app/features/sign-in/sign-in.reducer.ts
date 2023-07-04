import { createReducer, on } from "@ngrx/store";
import { SignInState } from "./sign-in.state";

import * as actions from './sign-in.actions';

const initialState = (undefined as unknown as SignInState)!;

export const reducer = createReducer(

  initialState,

  on(actions.configure, (_, action) => ({
    ...action.payload
  })),

  on(actions.dispose, () => initialState),

  on(actions.error, (state, action) => ({
    ...state,
    error: action.payload
  }))
);
