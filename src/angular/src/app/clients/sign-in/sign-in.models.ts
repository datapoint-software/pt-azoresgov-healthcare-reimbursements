export interface SignInModel {
  emailAddress: string;
  password: string;
  persistent: boolean;
};

export interface SignInOptionsResultModel {
  methods: SignInOptionsMethodsResultModel;
};

export interface SignInOptionsMethodsResultModel {
  basic?: SignInOptionsMethodsBasicResultModel;
};

export interface SignInOptionsMethodsBasicResultModel {
  persistentSessionsEnabled: boolean;
};

export interface SignInResultModel {
  session: SignInSessionResultModel;
  user: SignInUserResultModel;
};

export interface SignInSessionResultModel {
  id: string;
};

export interface SignInUserResultModel {
  id: string;
  name: string;
  emailAddress: string;
};
