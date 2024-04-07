import { Provider, inject } from "@angular/core";
import { ProcessCreationFeatureClient } from "../../api/features/process-creation/process-creation-feature.client";
import { IdentityFeature } from "../../features/identity/identity.feature";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";
import { ProcessCreationPatientSelectionFeature } from "../../features/process-creation-patient-selection/process-creation-patient-selection.feature";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

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
      ProcessCreationEntitySelectionFeature,
      ProcessCreationPatientSelectionFeature
    ];
  }
}
