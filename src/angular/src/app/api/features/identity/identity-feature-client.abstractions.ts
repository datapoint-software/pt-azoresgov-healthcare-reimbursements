import { UserRoleNature } from "../../../app.enums";

export interface IdentityFeatureRefreshResultModel {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration?: string;
  roles: UserRoleNature[];
}
