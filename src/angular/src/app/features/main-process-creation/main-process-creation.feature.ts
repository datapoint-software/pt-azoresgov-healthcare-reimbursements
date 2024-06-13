import { Injectable } from "@angular/core";
import { MainProcessCreationFeatureClient } from "@app/api/main-process-creation-feature/main-process-creation-feature.client";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { MainProcessCreationFeatureOptions, MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation-feature.abstractions";

@Injectable()
export class MainProcessCreationFeature {

  // #region State

  private _index: number = undefined!;

  private _steps: MainProcessCreationFeatureStep[] = undefined!;

  // #endregion

  // #region State accessors

  public get nextStep(): MainProcessCreationFeatureStep | null {
    return this._steps[this._index + 1] ?? null;
  }

  public get previousStep(): MainProcessCreationFeatureStep | null {
    return this._steps[this._index - 1] ?? null;
  }

  public get step(): Readonly<MainProcessCreationFeatureStep> {
    return this._steps[this._index];
  }

  public get stepCount(): number {
    return this._steps.length;
  }

  public get stepNumber(): number {
    return this._index + 1;
  }

  public get steps(): ReadonlyArray<MainProcessCreationFeatureStep> {
    return this._steps;
  }

  // #endregion

  // #region Actions

  public configure(options: MainProcessCreationFeatureOptions): void {
    this._index = this._steps.indexOf(options.step);
  }

  public async init(): Promise<void> {

    const options = await this._processCreationFeatureClient.getOptions();

    this._processCreationEntitySelectionFeature.configure({
      enabled: options.entitySelectionEnabled,
      entities: (options.entities ?? []).map(e => ({
        id: e.id,
        rowVersionId: e.rowVersionId,
        parentEntityId: e.parentEntityId ?? null,
        code: e.code,
        name: e.name,
        nature: e.nature
      })),
      entityId: options.entityId ?? null
    });

    this._processCreationPatientSelectionFeature.configure();

    this._index = 0;

    this._steps = [
      MainProcessCreationFeatureStep.PatientSelection,
      MainProcessCreationFeatureStep.Confirmation
    ];

    if (this._processCreationEntitySelectionFeature.enabled)
      this._steps.unshift(MainProcessCreationFeatureStep.EntitySelection);
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelectionFeature: MainProcessCreationEntitySelectionFeature,
    private readonly _processCreationPatientSelectionFeature: MainProcessCreationPatientSelectionFeature,
    private readonly _processCreationFeatureClient: MainProcessCreationFeatureClient
  ) {}
}
