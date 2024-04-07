import { Injectable } from "@angular/core";
import { ProcessCreationFeatureClient } from "../../api/features/process-creation/process-creation-feature.client";
import { ProcessCreationEntitySelectionFeature } from "../process-creation-entity-selection/process-creation-entity-selection.feature";

@Injectable()
export class ProcessCreationFeature {

  // #region State

  // #endregion

  // #region State accessors

  // #endregion

  // #region Actions

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
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelectionFeature: ProcessCreationEntitySelectionFeature,
    private readonly _processCreationFeatureClient: ProcessCreationFeatureClient
  ) {}
}
