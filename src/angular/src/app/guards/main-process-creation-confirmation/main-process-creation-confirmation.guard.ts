import { Injectable, inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";

@Injectable()
export class MainProcessCreationConfirmationGuard {

  public static canActivate(): boolean | UrlTree {

    const processCreation = inject(MainProcessCreationFeature);
    const router = inject(Router);

    if (!processCreation.patient)
      return router.createUrlTree([ '/processes', '_', 'patient-selection' ]);

    processCreation.step = MainProcessCreationFeatureStep.Confirmation;

    return true;
  }
}
