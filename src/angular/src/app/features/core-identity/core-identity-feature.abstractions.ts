import { UserRoleNature } from "@app/enums";

export type CoreIdentityFeatureClaims = {
  id: string;
  rowVersionId: string;
  name: string;
  emailAddress: string;
  expiration: Date | null;
  roles: UserRoleNature[];
}
