import { CanActivateFn } from "@angular/router";
import { IdentityFeature } from "./features/identity/identity.feature";
import { inject } from "@angular/core";

export const canActivateApp: CanActivateFn = async () => {
  await inject(IdentityFeature).init();
  return true;
}
