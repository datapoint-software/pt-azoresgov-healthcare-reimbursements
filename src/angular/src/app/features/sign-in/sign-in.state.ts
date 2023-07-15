import { ErrorModel } from "../../app.models";

export interface SignInState {
  authentication: {
    enabled: boolean;
    persistentEnabled: boolean;
    error?: ErrorModel;
  },
  redirectUrl?: string;
}
