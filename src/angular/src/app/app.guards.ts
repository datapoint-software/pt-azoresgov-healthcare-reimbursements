import { inject } from "@angular/core";
import { CanActivateFn } from "@angular/router";
import { IdentityFeature } from "./features/identity/identity.feature";

export const canActivateApp: CanActivateFn = async () => {
  await inject(IdentityFeature).init();
  return true;
}
