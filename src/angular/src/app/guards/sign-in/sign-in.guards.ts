import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";

export const canActivateSignIn: CanActivateFn = async (ar, r) => {
  await inject(SignInFeature).init(ar, r);
  return true;
};

export const canDeactivateSignIn: CanDeactivateFn<unknown> = async (c, ar, r) => {
  await inject(SignInFeature).dispose(ar, r);
  return true;
};
