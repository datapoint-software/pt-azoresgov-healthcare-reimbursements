import { Provider, inject } from "@angular/core";
import { MainProcessCreationEntitySelectionFeatureClient } from "@app/api/main-process-creation-entity-selection-feature/main-process-creation-entity-selection-feature.client";
import { MainProcessCreationFeatureClient } from "@app/api/main-process-creation-feature/main-process-creation-feature.client";
import { MainProcessCreationPatientSelectionFeatureClient } from "@app/api/main-process-creation-patient-selection-feature/main-process-creation-patient-selection-feature.client";
import { CoreIdentityFeature } from "@app/features/core-identity/core-identity.feature";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

export class MainProcessCreationGuard {

  public static async canActivate(): Promise<boolean> {

    const identity = inject(CoreIdentityFeature);
    const processCreation = inject(MainProcessCreationFeature);

    if (!identity.administrative)
      return false;

    await processCreation.init();

    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessCreationFeature,
      MainProcessCreationFeatureClient,
      MainProcessCreationEntitySelectionFeature,
      MainProcessCreationEntitySelectionFeatureClient,
      MainProcessCreationPatientSelectionFeature,
      MainProcessCreationPatientSelectionFeatureClient
    ];
  }
}
