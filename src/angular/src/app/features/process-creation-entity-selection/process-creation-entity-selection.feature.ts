import { Injectable } from "@angular/core";
import { Feature } from "../feature.abstract";
import { ProcessCreationEntitySelectionFeatureEntity, ProcessCreationEntitySelectionFeatureOptions } from "./process-creation-entity-selection-feature.abstractions";

@Injectable()
export class ProcessCreationEntitySelectionFeature implements Feature {

  // #region State

  private _enabled: boolean = undefined!;

  private _entities: Map<string, ProcessCreationEntitySelectionFeatureEntity> = undefined!;

  private _entityId: string | null = undefined!;

  // #endregion

  // #region State accessors

  public get complete(): boolean {
    return !!this._entityId;
  }

  public get enabled(): boolean {
    return this._enabled;
  }

  public get entity(): ProcessCreationEntitySelectionFeatureEntity | null {
    return (this._entityId && this._entities.get(this._entityId)) || null;
  }

  // #endregion

  // #region Actions

  public configure(options: ProcessCreationEntitySelectionFeatureOptions): void {
    this._enabled = options.enabled;
    this._entities = new Map<string, ProcessCreationEntitySelectionFeatureEntity>(options.entities.map(e => [ e.id, e ]));
    this._entityId = options.entityId;
  }

  public select(entityId: string): void {
    this._entityId = entityId;
  }

  // #endregion
}
