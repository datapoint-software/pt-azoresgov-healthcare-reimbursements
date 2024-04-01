import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { IdentityFeature } from "../../features/identity/identity.feature";

export class LayoutGuard {

  public static async canActivate(): Promise<UrlTree | boolean> {

    const identity = inject(IdentityFeature);
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
