import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCreationEntitySelectionFeatureClient } from "@app/api/main-process-creation-entity-selection-feature/main-process-creation-entity-selection-feature.client";
import { APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCreationEntitySelectionFeatureEntity, MainProcessCreationEntitySelectionFeatureForm, MainProcessCreationEntitySelectionFeatureOptions, MainProcessCreationEntitySelectionFeatureSearchResult } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection-feature.abstractions";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { Observable, Subject, debounceTime } from "rxjs";

@Injectable()
export class MainProcessCreationEntitySelectionFeature implements Feature {

  // #region State

  private _enabled: boolean = undefined!;

  private _entities: Map<string, MainProcessCreationEntitySelectionFeatureEntity> = undefined!;

  private _entityChanges: Subject<MainProcessCreationEntitySelectionFeatureEntity | null> = undefined!;

  private _entityId: string | null = undefined!;

  private _form: MainProcessCreationEntitySelectionFeatureForm = undefined!;

  private _searchResult: MainProcessCreationEntitySelectionFeatureSearchResult | null = undefined!;

  // #endregion

  // #region State accessors

  public get complete(): boolean {
    return !!this._entityId;
  }

  public get enabled(): boolean {
    return this._enabled;
  }

  public get entities(): ReadonlyMap<string, Readonly<MainProcessCreationEntitySelectionFeatureEntity>> {
    return this._entities;
  }

  public get entity(): Readonly<MainProcessCreationEntitySelectionFeatureEntity> | null {
    return (this._entityId && this._entities.get(this._entityId)) || null;
  }

  public get entityChanges(): Observable<MainProcessCreationEntitySelectionFeatureEntity | null> {
    return this._entityChanges;
  }

  public get form(): MainProcessCreationEntitySelectionFeatureForm {
    return this._form;
  }

  public get searchResult(): MainProcessCreationEntitySelectionFeatureSearchResult | null {
    return this._searchResult;
  }

  // #endregion

  // #region Actions

  public configure(options: MainProcessCreationEntitySelectionFeatureOptions): void {

    this._enabled = options.enabled;
    this._entities = new Map<string, MainProcessCreationEntitySelectionFeatureEntity>(options.entities.map(e => [ e.id, e ]));
    this._entityChanges = new Subject<MainProcessCreationEntitySelectionFeatureEntity | null>();
    this._entityId = options.entityId;
    this._searchResult = null;

    this._form = this._fb.group({
      filter: this._fb.control('', [ Validators.required, Validators.minLength(3), Validators.maxLength(128) ])
    });

    this._form.valueChanges
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .subscribe((value) => this._formValueChanges(value));
  }

  public async search(): Promise<void> {

    if (this._form.invalid)
      return;

    const loadingOverlayId = `${MainProcessCreationEntitySelectionFeature.name}/search`;

    this._loadingOverlay.enqueue(loadingOverlayId);

    await this._search(this._form.value.filter!);

    this._loadingOverlay.dequeue(loadingOverlayId);
  }

  public select(entityId: string): void {

    const hasChanges = this._entityId !== entityId;

    this._entityId = entityId;

    if (hasChanges)
      this._entityChanges.next(this._entities.get(entityId)!);
  }

  // #endregion

  private async _formValueChanges(value: Partial<{
    filter: string | null;
  }>): Promise<void> {

    if (this._form.invalid)
      return;

    return this._search(this._form.value.filter!);
  }

  private async _search(filter: string): Promise<void> {
    const response = await this._client.search({
      filter,
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
  }

  constructor(
    private readonly _client: MainProcessCreationEntitySelectionFeatureClient,
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: CoreLoadingOverlayFeature
  ) {}
}
