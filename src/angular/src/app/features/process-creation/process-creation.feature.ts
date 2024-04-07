import { Injectable } from "@angular/core";
import { ProcessCreationFeatureClient } from "../../api/features/process-creation/process-creation-feature.client";
import { ProcessCreationEntitySelectionFeature } from "../process-creation-entity-selection/process-creation-entity-selection.feature";
import { ProcessCreationFeatureOptions, ProcessCreationFeatureStep } from "./process-creation-feature.abstractions";

@Injectable()
export class ProcessCreationFeature {

  // #region State

  private _index: number = undefined!;

  private _steps: ProcessCreationFeatureStep[] = undefined!;

  // #endregion

  // #region State accessors

  public get nextStep(): ProcessCreationFeatureStep | null {
    return this._steps[this._index + 1] ?? null;
  }

  public get previousStep(): ProcessCreationFeatureStep | null {
    return this._steps[this._index - 1] ?? null;
  }

  public get step(): Readonly<ProcessCreationFeatureStep> {
    return this._steps[this._index];
  }

  public get stepCount(): number {
    return this._steps.length;
  }

  public get stepNumber(): number {
    return this._index + 1;
  }

  public get steps(): ReadonlyArray<ProcessCreationFeatureStep> {
    return this._steps;
  }

  // #endregion

  // #region Actions

  public configure(options: ProcessCreationFeatureOptions): void {
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

    this._index = 0;

    this._steps = [
      ProcessCreationFeatureStep.PatientSelection,
      ProcessCreationFeatureStep.Confirmation
    ];

    if (this._processCreationEntitySelectionFeature.enabled)
      this._steps.unshift(ProcessCreationFeatureStep.EntitySelection);
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelectionFeature: ProcessCreationEntitySelectionFeature,
    private readonly _processCreationFeatureClient: ProcessCreationFeatureClient
  ) {}
}
