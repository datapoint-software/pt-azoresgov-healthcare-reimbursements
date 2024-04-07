import { Provider, inject } from "@angular/core";
import { IdentityFeature } from "../../features/identity/identity.feature";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";
import { ProcessCreationFeatureClient } from "../../api/features/process-creation/process-creation-feature.client";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";

export class ProcessCreationGuard {

  public static async canActivate(): Promise<boolean> {

    const identity = inject(IdentityFeature);
    const processCreation = inject(ProcessCreationFeature);

    if (!identity.administrative)
      return false;

    await processCreation.init();

    return true;
  }

  public static get providers(): Provider[] {
    return [
      ProcessCreationFeature,
      ProcessCreationFeatureClient,
      ProcessCreationEntitySelectionFeature
    ];
  }
}
