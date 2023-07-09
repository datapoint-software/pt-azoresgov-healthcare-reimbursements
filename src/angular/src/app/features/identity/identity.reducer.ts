import { createReducer, on } from "@ngrx/store";
import { IdentityState } from "./identity.state";
import { authenticateConfigureSecrets, dispose, initConfigureWithSecrets, initConfigureWithoutSecrets, refreshConfigureSecrets } from "./identity.actions";

const initialState = (undefined as unknown as IdentityState);

export const reducer = createReducer(

  initialState,
  on(dispose, () => initialState),

  on(authenticateConfigureSecrets, (state, action) => ({
    ...state,
    secrets: {
      ...action.payload
    }
  })),

  on(initConfigureWithoutSecrets, () => ({})),

  on(initConfigureWithSecrets, (_, action) => ({
    secrets: {
      ...action.payload
    }
  })),

  on(refreshConfigureSecrets, (state, action) => ({
    ...state,
    secrets: {
      ...state.secrets!,
      ...action.payload
    }
  }))
);
