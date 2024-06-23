import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCaptureConfirmationGuard {

  public static canActivate(): boolean | UrlTree {

    const processCapture = inject(MainProcessCaptureFeature);
    const router = inject(Router);

    if (processCapture.seenSteps.size < 5)
      return router.createUrlTree([ '/processes', processCapture.process.id, 'capture' ]);

    processCapture.step = MainProcessCaptureFeatureStep.Confirmation;

    return true;
  }
}
