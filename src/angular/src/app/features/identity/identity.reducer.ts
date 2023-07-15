import { authenticate, dispose, initConfigure } from "./identity.actions";
import { createReducer, on } from "@ngrx/store";
import { IdentityState } from "./identity.state";

const initialState = undefined as unknown as IdentityState;

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(authenticate, (state, action) => ({
    ...state,
    claims: {
      ...action.payload
    }
  })),

  on(initConfigure, (_, action) => ({
    ...action.payload
  }))
);
