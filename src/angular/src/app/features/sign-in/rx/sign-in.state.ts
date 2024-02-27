import { ErrorResponseModel } from "../../../clients/api.models";
import { AuthenticationMethod } from "../../../enums/authentication-method.enum";

export interface SignInState {
  method: AuthenticationMethod;
  methods: {
    basic?: {
      persistentSessionsEnabled: boolean;
    };
  };
  error?: ErrorResponseModel;
  redirectUrl?: string;
};
