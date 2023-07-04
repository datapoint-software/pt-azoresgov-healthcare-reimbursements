export interface SignInAuthenticationOptionsResultModel {
  enabled: boolean;
  persistentEnabled: boolean;
}

export interface SignInModel {
  emailAddress: string;
  password: string;
}

export interface SignInOptionsResultModel {
  authentication: SignInAuthenticationOptionsResultModel;
}

export interface SignInResultModel {
  accessToken: string;
  accessTokenExpiration: number;
  refreshToken: string;
}
