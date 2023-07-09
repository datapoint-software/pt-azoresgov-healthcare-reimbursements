import { inject } from "@angular/core";
import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";
import { SignInComponent } from "./sign-in.component";

export const canActivateSignIn: CanActivateFn = async (route) => {

  await inject(SignInFeature).init({
    redirectUrl: route.queryParams['forward'] || undefined
  });

  return true;
}

export const canDeactivateSignIn: CanDeactivateFn<SignInComponent> = async () => {
  await inject(SignInFeature).dispose();
  return true;
}
