import { ErrorModel } from "../../app.models";

export interface SignInAuthenticationState {
  enabled: boolean;
  persistentEnabled: boolean;
}

export interface SignInState {
  authentication: SignInAuthenticationState;
  error?: ErrorModel;
  redirectUrl?: string;
}
