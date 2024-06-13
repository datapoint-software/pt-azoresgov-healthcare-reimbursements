import { UserRoleNature } from "@app/enums";

export type CoreIdentityFeatureRefreshResultModel = {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration?: string;
  roles: UserRoleNature[];
}
