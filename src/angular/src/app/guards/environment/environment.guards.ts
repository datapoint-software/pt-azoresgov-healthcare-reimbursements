import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { EnvironmentFeature } from "../../features/environment/environment.feature";
import { inject } from "@angular/core";

export const canActivateEnvironment: CanActivateFn = async (ar, r) => {
  await inject(EnvironmentFeature).init(ar, r);
  return true;
};

export const canDeactivateEnvironment: CanDeactivateFn<unknown> = async (c, ar, r) => {
  await inject(EnvironmentFeature).dispose(ar, r);
  return true;
};
