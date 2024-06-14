import { Injectable } from "@angular/core";
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

  // #endregion
}
