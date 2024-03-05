import { CanActivateFn } from "@angular/router";
import { IdentityFeature } from "../../features/identity/identity.feature";
import { inject } from "@angular/core";

export const authorize = (roles?: Array<string>): CanActivateFn => async (ar, r) => {
  return await inject(IdentityFeature).authorize(ar, r, roles);
};
