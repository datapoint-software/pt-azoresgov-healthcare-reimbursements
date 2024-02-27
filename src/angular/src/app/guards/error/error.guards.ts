import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { ErrorFeature } from "../../features/error/error.feature";
import { inject } from "@angular/core";

export const canActivateError: CanActivateFn = async (ar, r) => {
  await inject(ErrorFeature).init(ar, r);
  return true;
};

export const canDeactivateError: CanDeactivateFn<unknown> = async (c, ar, r) => {
  await inject(ErrorFeature).dispose(ar, r);
  return true;
};
