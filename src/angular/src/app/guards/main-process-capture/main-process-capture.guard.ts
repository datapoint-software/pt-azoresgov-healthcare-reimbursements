import { Provider, inject } from "@angular/core";
import { ActivatedRouteSnapshot } from "@angular/router";
import { MainProcessCaptureFeatureClient } from "@app/api/main-process-capture-feature/main-process-capture-feature.client";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCaptureGuard {

  public static async canActivate(route: ActivatedRouteSnapshot): Promise<boolean> {

    const processCapture = inject(MainProcessCaptureFeature);

    await processCapture.init(route.paramMap.get("processId")!);

    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessCaptureFeature,
      MainProcessCaptureFeatureClient
    ];
  }
}
