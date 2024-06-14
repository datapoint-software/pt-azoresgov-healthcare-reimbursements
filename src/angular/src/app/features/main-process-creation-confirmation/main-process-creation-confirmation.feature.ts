import { Injectable } from "@angular/core";
import { MainProcessCreationConfirmationFeatureClient } from "@app/api/main-process-creation-confirmation-feature/main-process-creation-confirmation-feature.client";
import { MainProcessCreationConfirmationFeatureOptions, MainProcessCreationConfirmationFeatureEntity, MainProcessCreationConfirmationFeaturePatient } from "@app/features/main-process-creation-confirmation/main-process-creation-confirmation-feature.abstractions";

@Injectable()
export class MainProcessCreationConfirmationFeature {

  // #region State

  private _entity: MainProcessCreationConfirmationFeatureEntity = undefined!;

  private _patient: MainProcessCreationConfirmationFeaturePatient = undefined!;

  // #endregion

  // #region State accessors

  public get entity(): MainProcessCreationConfirmationFeatureEntity {
    return this._entity;
  }

  public get patient(): MainProcessCreationConfirmationFeaturePatient {
    return this._patient;
  }

  // #endregion

  // #region Actions

  public configure(options: MainProcessCreationConfirmationFeatureOptions): void {
    this._entity = options.entity;
    this._patient = options.patient;
  }

  public async confirm(): Promise<void> {
    this._processCreationConfirmationFeatureClient.confirm({
      entityId: this._entity.id,
      entityRowVersionId: this._entity.rowVersionId,
      patientId: this._patient.id,
      patientRowVersionId: this._patient.rowVersionId
    });
  }

  // #endregion

  constructor(
    private readonly _processCreationConfirmationFeatureClient: MainProcessCreationConfirmationFeatureClient
  ) {}
}
