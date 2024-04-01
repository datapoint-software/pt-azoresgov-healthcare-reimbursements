import { UserRoleNature } from "../../app.enums";

export type IdentityFeatureClaims = {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration: Date | null;
  roles: UserRoleNature[];
}
