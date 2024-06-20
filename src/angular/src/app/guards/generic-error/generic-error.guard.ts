import { inject } from "@angular/core";
import { ActivatedRouteSnapshot } from "@angular/router";
import { CoreErrorFeature } from "@app/features/core-error/core-error.feature";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";

export class GenericErrorGuard {

  public static canActivate(route: ActivatedRouteSnapshot): boolean {

    const errorFeature = inject(CoreErrorFeature);
    const loadingOverlay = inject(CoreLoadingOverlayFeature);

    const statusCode = route.queryParamMap.get("statusCode");

    if (!errorFeature.error) {

      errorFeature.configure(
        route.queryParamMap.get("id"),
        route.queryParamMap.get("correlationId"),
        (statusCode && parseInt(statusCode!)) || null,
        route.queryParamMap.get("message"),
        route.queryParamMap.get("stackTrace")
      );
    }

    loadingOverlay.clear();

    return true;
  }
}
