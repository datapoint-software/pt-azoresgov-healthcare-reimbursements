import { inject } from "@angular/core";
import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { EnvironmentFeature } from "../../features/environment/environment.feature";
import { IdentityFeature } from "../../features/identity/identity.feature";

export const canActivateApp: CanActivateFn = async (ar, r) => {

  const features = [
    inject(EnvironmentFeature),
    inject(IdentityFeature)
  ];

  await Promise.all(features.map(f => f.init(ar, r)));

  return true;
};
