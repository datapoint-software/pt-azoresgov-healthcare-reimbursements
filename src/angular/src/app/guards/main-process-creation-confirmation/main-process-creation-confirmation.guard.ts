import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation-feature.abstractions";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

export class MainProcessCreationConfirmationGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(MainProcessCreationFeature);
    const processCreationPatientSelection = inject(MainProcessCreationPatientSelectionFeature);
    const router = inject(Router);

    if (!processCreationPatientSelection.complete)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    processCreation.configure({
      step: MainProcessCreationFeatureStep.Confirmation
    });

    return true;
  }

  private constructor() {}
}
