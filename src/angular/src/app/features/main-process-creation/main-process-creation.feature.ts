import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCreationFeatureEntityModel, MainProcessCreationFeaturePatientModel } from "@app/api/main-process-creation-feature/main-process-creation-feature-client.abstractions";
import { MainProcessCreationFeatureClient } from "@app/api/main-process-creation-feature/main-process-creation-feature.client";
import { APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCreationFeatureEntity, MainProcessCreationFeatureEntitySearchForm, MainProcessCreationFeatureEntitySearchResult, MainProcessCreationFeaturePatient, MainProcessCreationFeaturePatientSearchForm, MainProcessCreationFeaturePatientSearchResult, MainProcessCreationFeatureProcess, MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";
import { AppValidators } from "@app/validators";
import { debounceTime, filter } from "rxjs";

@Injectable()
export class MainProcessCreationFeature implements Feature {

  // #region State

  private _entities: Map<string, MainProcessCreationFeatureEntity> = undefined!;

  private _entityId: string | null = undefined!;

  private _entitySearchForm: MainProcessCreationFeatureEntitySearchForm = undefined!;

  private _entitySearchResult: MainProcessCreationFeatureEntitySearchResult | null = undefined!;

  private _index: number = undefined!;

  private _patientId: string | null = undefined!;

  private _patients: Map<string, MainProcessCreationFeaturePatient> = undefined!;

  private _patientSearchForm: MainProcessCreationFeaturePatientSearchForm = undefined!;

  private _patientSearchResult: MainProcessCreationFeaturePatientSearchResult | null = undefined!;

  private _process: MainProcessCreationFeatureProcess | null = undefined!;

  private _steps: MainProcessCreationFeatureStep[] = undefined!;

  // #endregion

  // #region State accessors

  public get entities(): ReadonlyMap<string, MainProcessCreationFeatureEntity> {
    return this._entities;
  }

  public get entity(): Readonly<MainProcessCreationFeatureEntity> | null {
    return (this._entityId && this._entities.get(this._entityId)) || null;
  }

  public get entitySearchForm(): MainProcessCreationFeatureEntitySearchForm {
    return this._entitySearchForm;
  }

  public get entitySearchResult(): Readonly<MainProcessCreationFeatureEntitySearchResult> | null {
    return this._entitySearchResult;
  }

  public get patient(): Readonly<MainProcessCreationFeaturePatient> | null {
    return (this._patientId && this._patients.get(this._patientId)) || null;
  }

  public get patients(): ReadonlyMap<string, MainProcessCreationFeaturePatient> {
    return this._patients;
  }

  public get patientSearchForm(): MainProcessCreationFeaturePatientSearchForm {
    return this._patientSearchForm;
  }

  public get patientSearchResult(): Readonly<MainProcessCreationFeaturePatientSearchResult> | null {
    return this._patientSearchResult;
  }

  public get process(): Readonly<MainProcessCreationFeatureProcess> | null {
    return this._process;
  }

  public get step(): MainProcessCreationFeatureStep {
    return this._steps[this._index];
  }

  public set step(step: MainProcessCreationFeatureStep) {

    const index = this._steps.indexOf(step);

    if (index < 0)
      throw new Error("Process creation step is out of bounds.");

    this._index = index;

    this._entitySearchForm.disable();
    this._patientSearchForm.disable();

    switch (this._steps[index]) {
      case MainProcessCreationFeatureStep.EntitySelection:
        this._entitySearchForm.enable();
        break;

      case MainProcessCreationFeatureStep.PatientSelection:
        this._patientSearchForm.enable();
        break;
    }
  }

  public get stepCount(): number {
    return this._steps.length;
  }

  public get stepNumber(): number {
    return this._index + 1;
  }

  // #endregion

  // #region Actions

  public async confirm(): Promise<void> {

    const id = `${MainProcessCreationFeature.name}.confirm`;

    this._loadingOverlay.enqueue(id);

    const entity = this.entity!;
    const patient = this.patient!;

    const response = await this._processCreationFeatureClient.confirm({
      entityId: entity.id,
      entityRowVersionId: entity.rowVersionId,
      patientId: patient.id,
      patientRowVersionId: patient.rowVersionId
    });

    this._process = response.process;

    this._loadingOverlay.dequeue(id);
  }

  public async init(): Promise<void> {

    this._entities = new Map();
    this._patients = new Map();
    this._process = null;
    this._index = 0;

    const options = await this._processCreationFeatureClient.getOptions();

    this._entitySearchForm = this._fb.group({
      filter: this._fb.control('', [ Validators.required, Validators.minLength(3), Validators.maxLength(128) ])
    });

    this._entitySearchForm.valueChanges
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .pipe(filter(() => this._entitySearchForm.valid))
      .subscribe(() => this._searchEntities());

    this._patientSearchForm = this._fb.group({
      filter: this._fb.control('', [ Validators.required, AppValidators.patientNumber ]),
      useFullSearchCriteria: this._fb.control(false, [ ])
    });

    this._patientSearchForm.controls.useFullSearchCriteria.valueChanges
      .subscribe((useFullSearchCriteria) => this._patientSearchFormUseFullSearchCriteriaChanges(useFullSearchCriteria));

    this._patientSearchForm.valueChanges
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .pipe(filter(() => this._patientSearchForm.valid))
      .subscribe(() => this._searchPatients());

    this._entitySearchForm.disable();
    this._patientSearchForm.disable();

    this._entitySearchResult = null;
    this._patientSearchResult = null;

    this._steps = [
      MainProcessCreationFeatureStep.PatientSelection,
      MainProcessCreationFeatureStep.Confirmation
    ];

    // Server will provide us with a pre-selected entity when
    // the current user profile only has access to a single entity.
    if (options.entitySelection) {

      this._entityId = options.entitySelection.entityId;
      this._entitySearchForm.disable();

      this._setEntities(options.entitySelection.entities);

    } else {
      this._entityId = null;
      this._steps.unshift(MainProcessCreationFeatureStep.EntitySelection);
    }

    this._patientId = null;
  }

  public async searchEntities(): Promise<void> {

    if (this._entitySearchForm.invalid)
      return;

    const id = `${MainProcessCreationFeature.name}.searchEntities`;

    this._loadingOverlay.enqueue(id);

    await this._searchEntities();

    this._loadingOverlay.dequeue(id);
  }

  public async searchPatients(): Promise<void> {

    if (this._patientSearchForm.invalid)
      return;

    const id = `${MainProcessCreationFeature.name}.searchPatients`;

    this._loadingOverlay.enqueue(id);

    await this._searchPatients();

    this._loadingOverlay.dequeue(id);
  }

  public selectEntity(entityId: string): void {
    this._entityId = entityId;
  }

  public selectPatient(patientId: string): void {
    this._patientId = patientId;
  }

  // #endregion

  // #region Queries

  public isPatientSelectable(patientId: string): boolean {
    const patient = this._patients.get(patientId)!;
    return !patient.external && !patient.death ;
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: CoreLoadingOverlayFeature,
    private readonly _processCreationFeatureClient: MainProcessCreationFeatureClient
  ) {}

  private _patientSearchFormUseFullSearchCriteriaChanges(useFullSearchCriteria: boolean | null): void {

    this._patientSearchForm.controls.filter.clearValidators();

    this._patientSearchForm.controls.filter.addValidators(useFullSearchCriteria ? [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(128)
    ] : [
      Validators.required,
      AppValidators.patientNumber
    ]);

    this._patientSearchForm.controls.filter.updateValueAndValidity();
  }

  private _setEntities(entities: MainProcessCreationFeatureEntityModel[]): void {
    for (const entity of entities) {
      this._entities.set(entity.id, ({
        id: entity.id,
        rowVersionId: entity.rowVersionId,
        parentEntityId: entity.parentEntityId ?? null,
        code: entity.code,
        name: entity.name,
        nature: entity.nature
      }));
    }
  }

  private _setPatients(patients: MainProcessCreationFeaturePatientModel[]): void {
    for (const patient of patients) {
      this._patients.set(patient.id, ({
        id: patient.id,
        rowVersionId: patient.rowVersionId,
        entityId: patient.entityId,
        number: patient.number,
        taxNumber: patient.taxNumber ?? null,
        name: patient.name,
        birth: patient.birth ?? null,
        death: patient.death ?? null,
        faxNumber: patient.faxNumber ?? null,
        mobileNumber: patient.mobileNumber ?? null,
        phoneNumber: patient.phoneNumber ?? null,
        emailAddress: patient.emailAddress ?? null,
        external: patient.external
      }));
    }
  }

  private async _searchEntities(): Promise<void> {

    const entitySearchResult = await this._processCreationFeatureClient.searchEntities({
      filter: this._entitySearchForm.value.filter!,
      skip: 0,
      take: 5
    });

    this._setEntities(entitySearchResult.entities);

    this._entitySearchResult = ({
      totalMatchCount: entitySearchResult.totalMatchCount,
      entityIds: entitySearchResult.entityIds
    });
  }

  private async _searchPatients(): Promise<void> {

    const patientSearchResult = await this._processCreationFeatureClient.searchPatients({
      entityId: this._entityId!,
      entityRowVersionId: this._entities.get(this._entityId!)!.rowVersionId,
      filter: this._patientSearchForm.value.filter!,
      useFullSearchCriteria: this._patientSearchForm.value.useFullSearchCriteria || false,
      skip: 0,
      take: 5
    });

    this._setEntities(patientSearchResult.entities);
    this._setPatients(patientSearchResult.patients);

    this._patientSearchResult = ({
      totalMatchCount: patientSearchResult.totalMatchCount,
      patientIds: patientSearchResult.patientIds
    });
  }

}
