export interface SignInModel {
  emailAddress: string;
  password: string;
  persistent: boolean;
}

export interface SignInOptionsMethodsBasicResultModel {
  persistentSessionsEnabled: boolean;
}

export interface SignInOptionsMethodsResultModel {
  basic?: SignInOptionsMethodsBasicResultModel;
}

export interface SignInOptionsResultModel {
  methods: SignInOptionsMethodsResultModel;
}

export interface SignInResultModel {
  roles: SignInRoleResultModel[];
  session: SignInSessionResultModel;
  user: SignInUserResultModel;
}

export interface SignInRoleResultModel {
  id: string;
  rowVersionId: string;
  name: string;
}

export interface SignInSessionResultModel {
  id: string;
}

export interface SignInUserResultModel {
  id: string;
  name: string;
  emailAddress: string;
}
