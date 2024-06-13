import { UserRoleNature } from "@app/enums";

export type GenericSignInFeatureOptionsModel = {
  persistentSessionsEnabled: boolean;
}

export type GenericSignInFeatureSignInModel = {
  emailAddress: string;
  password: string;
  persistent: boolean;
}

export type GenericSignInFeatureSignInResultModel = {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration?: string;
  roles: UserRoleNature[];
}
