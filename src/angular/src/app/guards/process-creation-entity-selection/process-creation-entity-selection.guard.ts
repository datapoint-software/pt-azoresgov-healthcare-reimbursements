import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";
import { ProcessCreationFeatureStep } from "../../features/process-creation/process-creation-feature.abstractions";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

export class ProcessCreationEntitySelectionGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(ProcessCreationFeature);
    const processCreationEntitySelection = inject(ProcessCreationEntitySelectionFeature);
    const router = inject(Router);

    if (!processCreationEntitySelection.enabled)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    processCreation.configure({
      step: ProcessCreationFeatureStep.EntitySelection
    });

    return true;
  }
}
