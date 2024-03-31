import { Provider, inject } from "@angular/core";
import { ActivatedRouteSnapshot } from "@angular/router";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";

export class SignInGuard {

  public static async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {

    const signIn = inject(SignInFeature);

    const redirectUrl = route.queryParamMap.get("redirect");

    await signIn.init(redirectUrl);

    return true;
  }

  public static get providers(): Provider[] {
    return [
      SignInFeature
    ];
  }

  private constructor() {}
}
