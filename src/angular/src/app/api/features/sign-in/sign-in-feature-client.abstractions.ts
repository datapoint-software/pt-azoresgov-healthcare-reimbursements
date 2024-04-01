export interface SignInFeatureOptionsModel {
  persistentSessionsEnabled: boolean;
}

export interface SignInFeatureSignInModel {
  emailAddress: string;
  password: string;
  persistent: boolean;
}

export interface SignInFeatureSignInResultModel {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration?: string;
}
