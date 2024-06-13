import { Provider, inject } from "@angular/core";
import { ActivatedRouteSnapshot } from "@angular/router";
import { GenericSignInFeatureClient } from "@app/api/generic-sign-in-feature/generic-sign-in-feature.client";
import { GenericSignInFeature } from "@app/features/generic-sign-in/generic-sign-in.feature";

export class GenericSignInGuard {

  public static async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {

    const signIn = inject(GenericSignInFeature);

    const redirectUrl = route.queryParamMap.get("redirect");

    await signIn.init(redirectUrl);

    return true;
  }

  public static get providers(): Provider[] {
    return [
      GenericSignInFeature,
      GenericSignInFeatureClient
    ];
  }

  private constructor() {}
}
