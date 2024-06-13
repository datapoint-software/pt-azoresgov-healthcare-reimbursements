import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { CoreIdentityFeature } from "@app/features/core-identity/core-identity.feature";

export class MainGuard {

  public static async canActivate(): Promise<UrlTree | boolean> {

    const identity = inject(CoreIdentityFeature);
    const router = inject(Router);

    if (identity.anonymous) {

      await identity.refresh();

      if (identity.anonymous) {
        return router.createUrlTree([
          '/sign-in'
        ], {
          queryParams: {
            redirect: document.location.pathname
          }
        });
      }
    }

    return true;
  }
}
