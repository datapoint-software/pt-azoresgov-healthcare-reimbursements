import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation-feature.abstractions";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

export class MainProcessCreationEntitySelectionGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(MainProcessCreationFeature);
    const processCreationEntitySelection = inject(MainProcessCreationEntitySelectionFeature);
    const router = inject(Router);

    if (!processCreationEntitySelection.enabled)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    processCreation.configure({
      step: MainProcessCreationFeatureStep.EntitySelection
    });

    return true;
  }
}
