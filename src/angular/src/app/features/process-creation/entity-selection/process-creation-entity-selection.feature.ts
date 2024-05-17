import { Injectable } from "@angular/core";
import { Feature } from "../../feature.abstract";
import { ProcessCreationEntitySelectionFeatureEntity, ProcessCreationEntitySelectionFeatureForm, ProcessCreationEntitySelectionFeatureOptions, ProcessCreationEntitySelectionFeatureSearchResult } from "./process-creation-entity-selection-feature.abstractions";
import { FormBuilder, Validators } from "@angular/forms";
import { ProcessCreationEntitySelectionFeatureClient } from "../../../api/features/process-creation/entity-selection/process-creation-entity-selection-feature.client";
import { LoadingOverlayFeature } from "../../loading-overlay/loading-overlay.feature";

@Injectable()
export class ProcessCreationEntitySelectionFeature implements Feature {

  // #region State

  private _enabled: boolean = undefined!;

  private _entities: Map<string, ProcessCreationEntitySelectionFeatureEntity> = undefined!;

  private _entityId: string | null = undefined!;

  private _form: ProcessCreationEntitySelectionFeatureForm = undefined!;

  private _searchResult: ProcessCreationEntitySelectionFeatureSearchResult | null = undefined!;

  // #endregion

  // #region State accessors

  public get complete(): boolean {
    return !!this._entityId;
  }

  public get enabled(): boolean {
    return this._enabled;
  }

  public get entities(): ReadonlyMap<string, Readonly<ProcessCreationEntitySelectionFeatureEntity>> {
    return this._entities;
  }

  public get entity(): ProcessCreationEntitySelectionFeatureEntity | null {
    return (this._entityId && this._entities.get(this._entityId)) || null;
  }

  public get form(): ProcessCreationEntitySelectionFeatureForm {
    return this._form;
  }

  public get searchResult(): ProcessCreationEntitySelectionFeatureSearchResult | null {
    return this._searchResult;
  }

  // #endregion

  // #region Actions

  public configure(options: ProcessCreationEntitySelectionFeatureOptions): void {

    this._enabled = options.enabled;
    this._entities = new Map<string, ProcessCreationEntitySelectionFeatureEntity>(options.entities.map(e => [ e.id, e ]));
    this._entityId = options.entityId;
    this._searchResult = null;

    this._form = this._fb.group({
      filter: this._fb.control('', [ Validators.required, Validators.minLength(3), Validators.maxLength(128) ])
    });
  }

  public async search(): Promise<void> {

    if (this._form.invalid)
      return;

    const loadingOverlayId = `${ProcessCreationEntitySelectionFeature.name}/search`;

    this._loadingOverlay.enqueue(loadingOverlayId);

    const response = await this._client.search({
      filter: this._form.value.filter!,
      skip: 0,
      take: 5
    });

    for (const entity of response.entities) {
      this._entities.set(entity.id, {
        id: entity.id,
        rowVersionId: entity.rowVersionId,
        parentEntityId: entity.parentEntityId ?? null,
        code: entity.code,
        name: entity.name,
        nature: entity.nature
      });
    }

    this._searchResult = {
      entityIds: response.entityIds,
      totalMatchCount: response.totalMatchCount
    };

    this._loadingOverlay.dequeue(loadingOverlayId);
  }

  public select(entityId: string): void {
    this._entityId = entityId;
  }

  // #endregion

  constructor(
    private readonly _client: ProcessCreationEntitySelectionFeatureClient,
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: LoadingOverlayFeature
  ) {}
}
