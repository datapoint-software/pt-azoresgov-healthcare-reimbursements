import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessSearchFeatureClient } from "@app/api/main-process-search-feature/main-process-search-feature.client";
import { APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { MainProcessSearchFeatureEntity, MainProcessSearchFeaturePatient, MainProcessSearchFeatureProcess, MainProcessSearchFeatureProcessSearchForm, MainProcessSearchFeatureProcessSearchResult } from "@app/features/main-process-search/main-process-search-feature.abstractions";
import { debounceTime, filter, tap } from "rxjs";

@Injectable()
export class MainProcessSearchFeature {

  // #region State

  private _entities: Map<string, MainProcessSearchFeatureEntity> = undefined!;

  private _patients: Map<string, MainProcessSearchFeaturePatient> = undefined!;

  private _processes: Map<string, MainProcessSearchFeatureProcess> = undefined!;

  private _processSearchForm: MainProcessSearchFeatureProcessSearchForm = undefined!;

  private _processSearchResult: MainProcessSearchFeatureProcessSearchResult | null = undefined!;

  // #endregion

  // #region State accessors

  public get entities(): ReadonlyMap<string, Readonly<MainProcessSearchFeatureEntity>> {
    return this._entities;
  }

  public get patients(): ReadonlyMap<string, Readonly<MainProcessSearchFeaturePatient>> {
    return this._patients;
  }

  public get processes(): ReadonlyMap<string, Readonly<MainProcessSearchFeatureProcess>> {
    return this._processes;
  }

  public get processSearchForm(): MainProcessSearchFeatureProcessSearchForm {
    return this._processSearchForm;
  }

  public get processSearchResult(): Readonly<MainProcessSearchFeatureProcessSearchResult> | null {
    return this._processSearchResult;
  }

  public get processSearchResultLeftoverMatchCount(): number | null {

    if (this._processSearchResult === null)
      return null;

    return this._processSearchResult.totalMatchCount - this._processSearchResult.processIds.size;
  }

  // #endregion

  // #region Actions

  public init(): void {

    this._entities = new Map();
    this._patients = new Map();
    this._processes = new Map();

    this._processSearchResult = null;

    this._processSearchForm = this._fb.group({
      filter: this._fb.control('', [ Validators.minLength(3), Validators.maxLength(128) ]),
      useFullSearchCriteria: this._fb.control(false, [ ])
    });

    this._processSearchForm.valueChanges
      .pipe(tap(() => this._processSearchResult = null))
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .pipe(filter(() => this._processSearchForm.valid))
      .subscribe(() => this._searchProcesses());

  }

  public async searchProcesses(): Promise<void> {

    if (this._processSearchForm.invalid)
      return;

    const id = `${MainProcessSearchFeature.name}.searchProcesses`;

    this._loadingOverlay.enqueue(id);

    await this._searchProcesses();

    this._loadingOverlay.dequeue(id);
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: CoreLoadingOverlayFeature,
    private readonly _processSearchFeatureClient: MainProcessSearchFeatureClient
  ) {}

  private async _searchProcesses(): Promise<void> {

    if (this._processSearchResult) {

      const { processIds, totalMatchCount } = this._processSearchResult;

      if (totalMatchCount === processIds.size)
        return;
    }

    const filter = this._processSearchForm.value.filter;

    const response = await this._processSearchFeatureClient.searchProcesses({
      filter: filter ?? undefined,
      useFullSearchCriteria: this._processSearchForm.value.useFullSearchCriteria || false,
      skip: this._processSearchResult?.processIds.size ?? 0,
      take: 25
    });

    for (const entity of response.entities) {
      this._entities.set(entity.id, {
        id: entity.id,
        rowVersionId: entity.rowVersionId,
        parentEntityId: entity.parentEntityId ?? null,
        code: entity.code,
        name: entity.name,
        nature: entity.nature,
      });
    }

    for (const patient of response.patients) {
      this._patients.set(patient.id, {
        id: patient.id,
        rowVersionId: patient.rowVersionId,
        entityId: patient.entityId,
        number: patient.number,
        taxNumber: patient.taxNumber,
        name: patient.name,
        death: patient.death ?? null
      });
    }

    for (const process of response.processes) {
      this._processes.set(process.id, {
        id: process.id,
        rowVersionId: process.rowVersionId,
        entityId: process.entityId,
        patientId: process.patientId,
        number: process.number,
        status: process.status,
        creation: process.creation
      });
    }

    // We need to ensure the response we're processing is actually
    // the one matching our query.
    if (filter === this._processSearchForm.value.filter) {
      this._processSearchResult = ({
        processIds: this._processSearchResult?.processIds.size
          ? new Set([ ...this._processSearchResult.processIds, ...response.processIds ])
          : new Set(response.processIds),
        totalMatchCount: response.totalMatchCount
      });
    }
  }
}
