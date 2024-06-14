import { Provider, inject } from "@angular/core";
import { Router, UrlTree } from "@angular/router";
import { MainProcessCreationConfirmationFeatureClient } from "@app/api/main-process-creation-confirmation-feature/main-process-creation-confirmation-feature.client";
import { MainProcessCreationConfirmationFeature } from "@app/features/main-process-creation-confirmation/main-process-creation-confirmation.feature";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation-feature.abstractions";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

export class MainProcessCreationConfirmationGuard {

  public static canActivate(): UrlTree | boolean {

    const processCreation = inject(MainProcessCreationFeature);
    const processCreationEntitySelection = inject(MainProcessCreationEntitySelectionFeature);
    const processCreationPatientSelection = inject(MainProcessCreationPatientSelectionFeature);
    const processCreationPatientConfirmation = inject(MainProcessCreationConfirmationFeature);
    const router = inject(Router);

    if (!processCreationPatientSelection.complete)
      return router.createUrlTree([ '/processes', '_', 'patient' ]);

    processCreation.configure({
      step: MainProcessCreationFeatureStep.Confirmation
    });

    const entity = processCreationEntitySelection.entity!;
    const parentEntity = processCreationEntitySelection.entities.get(entity.parentEntityId!) ?? null;

    processCreationPatientConfirmation.configure({
      entity: {
        id: entity.id,
        rowVersionId: entity.rowVersionId,
        code: entity.code,
        name: entity.name,
        nature: entity.nature,
        parentEntity: parentEntity && {
          id: parentEntity.id,
          rowVersionId: parentEntity.rowVersionId,
          code: parentEntity.code,
          name: parentEntity.name,
          nature: parentEntity.nature,
          parentEntity: null
        }
      },
      patient: processCreationPatientSelection.patient!
    });

    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessCreationConfirmationFeature,
      MainProcessCreationConfirmationFeatureClient
    ]
  };

  private constructor() {}
}
