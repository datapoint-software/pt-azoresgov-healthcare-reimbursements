export interface SignInAuthenticationOptionsResultModel {
  enabled: boolean;
  persistentEnabled: boolean;
}

export interface SignInEntityResultModel {
  id: string;
  permissions: SignInPermissionResultModel[];
}

export interface SignInModel {
  emailAddress: string;
  password: string;
  persistent: boolean;
}

export interface SignInOptionsResultModel {
  authentication: SignInAuthenticationOptionsResultModel;
}

export interface SignInPermissionResultModel {
  id: string;
  name: string;
}

export interface SignInResultModel {
  entities: SignInEntityResultModel[];
  permissions: SignInPermissionResultModel[];
  user: SignInUserResultModel;
  userSession: SignInUserSessionResultModel;
}

export interface SignInUserResultModel {
  id: string;
  name: string;
}

export interface SignInUserSessionResultModel {
  id: string;
}
