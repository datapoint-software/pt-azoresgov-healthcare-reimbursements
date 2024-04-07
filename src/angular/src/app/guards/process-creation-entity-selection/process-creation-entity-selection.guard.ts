import { inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";

export class ProcessCreationEntitySelectionGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreationEntitySelection = inject(ProcessCreationEntitySelectionFeature);
    const router = inject(Router);

    if (!processCreationEntitySelection.enabled)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    return true;
  }
}
