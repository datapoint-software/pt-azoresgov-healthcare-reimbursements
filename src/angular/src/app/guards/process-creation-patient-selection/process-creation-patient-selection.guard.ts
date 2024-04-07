import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";
import { ProcessCreationFeatureStep } from "../../features/process-creation/process-creation-feature.abstractions";

export class ProcessCreationPatientSelectionGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(ProcessCreationFeature);
    const processCreationEntitySelection = inject(ProcessCreationEntitySelectionFeature);
    const router = inject(Router);

    if (!processCreationEntitySelection.complete)
      return router.createUrlTree([ '/processes', '_', 'entity' ]);

    processCreation.configure({
      step: ProcessCreationFeatureStep.PatientSelection
    });

    return true;
  }

  private constructor() {}
}
