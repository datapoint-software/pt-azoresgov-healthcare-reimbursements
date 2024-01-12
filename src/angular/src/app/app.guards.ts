import { CanActivateFn } from "@angular/router";
import { IdentityFeature } from "./features/identity/identity.feature";
import { inject } from "@angular/core";
import { EnvironmentFeature } from "./features/environment/environment.feature";

export const canActivateApp: CanActivateFn = async () => {

  await Promise.all([
    inject(EnvironmentFeature).init(),
    inject(IdentityFeature).init()
  ]);

  return true;
}
