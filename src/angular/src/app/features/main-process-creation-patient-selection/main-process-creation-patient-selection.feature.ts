import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCreationPatientSelectionFeatureSearchModeModel } from "@app/api/main-process-creation-patient-selection-feature/main-process-creation-patient-selection-feature-client.abstractions";
import { MainProcessCreationPatientSelectionFeatureClient } from "@app/api/main-process-creation-patient-selection-feature/main-process-creation-patient-selection-feature.client";
import { APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCreationEntitySelectionFeatureEntity } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection-feature.abstractions";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationPatientSelectionFeatureForm, MainProcessCreationPatientSelectionFeaturePatient, MainProcessCreationPatientSelectionFeatureSearchResult } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection-feature.abstractions";
import { debounceTime } from "rxjs";

@Injectable()
export class MainProcessCreationPatientSelectionFeature implements Feature {

  // #region State

  private _form: MainProcessCreationPatientSelectionFeatureForm = undefined!;

  private _patientId: string | null = undefined!;

  private _patients: Map<string, MainProcessCreationPatientSelectionFeaturePatient> = undefined!;

  private _searchResult: MainProcessCreationPatientSelectionFeatureSearchResult | null = undefined!;

  // #endregion

  // #region State accessors

  public get complete(): boolean {
    return !!this._patientId;
  }

  public get form(): MainProcessCreationPatientSelectionFeatureForm {
    return this._form;
  }

  public get patient(): Readonly<MainProcessCreationPatientSelectionFeaturePatient> | null {

    if (!this._patientId)
      return null;

    return this._patients.get(this._patientId) ?? null;
  }

  public get patients(): ReadonlyMap<string, Readonly<MainProcessCreationPatientSelectionFeaturePatient>> {
    return this._patients;
  }

  public get searchResult(): Readonly<MainProcessCreationPatientSelectionFeatureSearchResult> | null {
    return this._searchResult;
  }

  // #endregion

  // #region Actions

  public configure(): void {

    this._form = this._fb.group({
      filter: this._fb.control('', [ Validators.required, Validators.minLength(3), Validators.maxLength(128) ]),
      full: this._fb.control(false, [ Validators.required ])
    });

    this._form.valueChanges
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .subscribe((values) => this._formValueChanges(values))

    this._patients = new Map<string, MainProcessCreationPatientSelectionFeaturePatient>();
    this._searchResult = null;

    this._processCreationEntitySelectionFeature.entityChanges
      .subscribe((entity) => this._entityChanges(entity));
  }

  public async search(): Promise<void> {

    if (this._form.invalid)
      return;

    const loadingOverlayId = `${MainProcessCreationPatientSelectionFeature.name}/search`;

    this._loadingOverlay.enqueue(loadingOverlayId);

    await this._search(this.form.value);

    this._loadingOverlay.dequeue(loadingOverlayId);
  }

  public select(patientId: string): void {
    this._patientId = patientId;
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: CoreLoadingOverlayFeature,
    private readonly _processCreationEntitySelectionFeature: MainProcessCreationEntitySelectionFeature,
    private readonly _processCreationPatientSelectionFeatureClient: MainProcessCreationPatientSelectionFeatureClient
  ) {}

  private _entityChanges(entity: MainProcessCreationEntitySelectionFeatureEntity | null) {
    this._form.reset({ filter: '', full: false }, { emitEvent: false });
    this._patientId = null;
    this._patients.clear();
    this._searchResult = null;
  }

  private async _formValueChanges(values: Partial<{
    filter: string | null;
    full: boolean | null;
  }>): Promise<void> {

    if (this._form.invalid)
      return;

    return this._search(values);
  }

  private async _search(values: Partial<{
    filter: string | null;
    full: boolean | null;
  }>): Promise<void> {

    const result = await this._processCreationPatientSelectionFeatureClient.search({
      entityId: this._processCreationEntitySelectionFeature.entity!.id,
      entityRowVersionId: this._processCreationEntitySelectionFeature.entity!.rowVersionId,
      filter: values.filter!,
      mode: values.full ? MainProcessCreationPatientSelectionFeatureSearchModeModel.Full : MainProcessCreationPatientSelectionFeatureSearchModeModel.PatientNumber
    });

    for (const p of result.patients) {
      this._patients.set(p.id, {
        id: p.id,
        rowVersionId: p.rowVersionId,
        number: p.number,
        taxNumber: p.taxNumber ?? null,
        name: p.name,
        birth: p.birth ?? null,
        death: p.death ?? null,
        external: p.external,
        faxNumber: p.faxNumber ?? null,
        mobileNumber: p.mobileNumber ?? null,
        phoneNumber: p.phoneNumber ?? null,
        emailAddress: p.emailAddress ?? null,
        postalAddressArea: p.postalAddressArea ?? null,
        postalAddressAreaCode: p.postalAddressAreaCode ?? null,
        postalAddressLine1: p.postalAddressLine1 ?? null,
        postalAddressLine2: p.postalAddressLine2 ?? null,
        postalAddressLine3: p.postalAddressLine3 ?? null
      });
    }

    this._searchResult = {
      patientIds: result.patientIds,
      totalMatchCount: result.totalMatchCount
    };
  }
}
