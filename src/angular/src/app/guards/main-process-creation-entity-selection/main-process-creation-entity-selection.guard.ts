import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";

export class MainProcessCreationEntitySelectionGuard {

  public static canActivate(): boolean | UrlTree {

    const processCreation = inject(MainProcessCreationFeature);
    const router = inject(Router);

    if (processCreation.entitySearchForm.disabled)
      return router.createUrlTree([ '/processes', '_', 'patient-selection' ]);

    processCreation.step = MainProcessCreationFeatureStep.EntitySelection;

    return true;
  }
}
