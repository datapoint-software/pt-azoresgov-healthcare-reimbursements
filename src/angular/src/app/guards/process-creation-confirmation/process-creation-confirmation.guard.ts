import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { ProcessCreationPatientSelectionFeature } from "../../features/process-creation-patient-selection/process-creation-patient-selection.feature";
import { ProcessCreationFeatureStep } from "../../features/process-creation/process-creation-feature.abstractions";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

export class ProcessCreationConfirmationGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(ProcessCreationFeature);
    const processCreationPatientSelection = inject(ProcessCreationPatientSelectionFeature);
    const router = inject(Router);

    if (!processCreationPatientSelection.complete)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    processCreation.configure({
      step: ProcessCreationFeatureStep.Confirmation
    });

    return true;
  }

  private constructor() {}
}
