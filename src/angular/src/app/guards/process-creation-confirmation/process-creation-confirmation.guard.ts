import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { ProcessCreationPatientSelectionFeature } from "../../features/process-creation-patient-selection/process-creation-patient-selection.feature";

export class ProcessCreationConfirmationGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreationPatientSelection = inject(ProcessCreationPatientSelectionFeature);
    const router = inject(Router);

    if (processCreationPatientSelection.complete)
      return true;

    return router.createUrlTree([
      '/processes',
      '_',
      'patient'
    ]);
  }

  private constructor() {}
}
