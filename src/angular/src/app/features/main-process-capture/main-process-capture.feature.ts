import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCaptureFeatureClient } from "@app/api/main-process-capture-feature/main-process-capture-feature.client";
import { APP_AUTO_SAVE_DEBOUNCE_TIME, APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { ProcessPaymentMethod, ProcessPaymentRecipient } from "@app/enums";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { CoreTaskOverlayFeature } from "@app/features/core-task-overlay/core-task-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCaptureFeatureEntity, MainProcessCaptureFeatureForm, MainProcessCaptureFeatureLegalRepresentative, MainProcessCaptureFeaturePatient, MainProcessCaptureFeatureProcess, MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { isSameObject } from "@app/helpers";
import { AppValidators } from "@app/validators";
import { debounceTime, filter, map, mergeMap, tap } from "rxjs";

const __EEF = { emitEvent: false };

@Injectable()
export class MainProcessCaptureFeature implements Feature {

  // #region State

  private _entities: Map<string, MainProcessCaptureFeatureEntity> = undefined!;

  private _form: MainProcessCaptureFeatureForm = undefined!;

  private _legalRepresentative: MainProcessCaptureFeatureLegalRepresentative | null = undefined!;

  private _patient: MainProcessCaptureFeaturePatient = undefined!;

  private _process: MainProcessCaptureFeatureProcess = undefined!;

  private _seenSteps: Set<MainProcessCaptureFeatureStep> = undefined!;

  private _step: MainProcessCaptureFeatureStep | null = undefined!;

  // #endregion

  // #region State accessors

  public get entities(): ReadonlyMap<string, Readonly<MainProcessCaptureFeatureEntity>> {
    return this._entities;
  }

  public get form(): MainProcessCaptureFeatureForm {
    return this._form;
  }

  public get patient(): Readonly<MainProcessCaptureFeaturePatient> {
    return this._patient;
  }

  public get process(): Readonly<MainProcessCaptureFeatureProcess> {
    return this._process;
  }

  public get seenSteps(): ReadonlySet<MainProcessCaptureFeatureStep> {
    return this._seenSteps;
  }

  public get step(): MainProcessCaptureFeatureStep | null {
    return this._step;
  }

  public set step(step: MainProcessCaptureFeatureStep) {
    this._seenSteps.has(step) || this._seenSteps.add(step);
    this._step = step;
  }

  // #endregion

  // #region Actions

  public async init(processId: string): Promise<void> {

    const options = await this._processCaptureFeatureClient.getOptions({
      processId
    });

    this._entities = new Map();

    for (const entity of options.entities) {
      this._entities.set(entity.id, {
        id: entity.id,
        rowVersionId: entity.rowVersionId,
        parentEntityId: entity.parentEntityId ?? null,
        code: entity.code,
        name: entity.name,
        nature: entity.nature
      });
    }

    this._patient = ({
      id: options.patient.id,
      rowVersionId: options.patient.rowVersionId,
      entityId: options.patient.entityId,
      number: options.patient.number,
      taxNumber: options.patient.taxNumber,
      name: options.patient.name,
      birth: options.patient.birth,
      death: options.patient.death ?? null,
      faxNumber: options.patient.faxNumber ?? null,
      mobileNumber: options.patient.mobileNumber ?? null,
      phoneNumber: options.patient.phoneNumber ?? null,
      emailAddress: options.patient.emailAddress ?? null,
      postalAddressArea: options.patient.postalAddressArea,
      postalAddressAreaCode: options.patient.postalAddressAreaCode,
      postalAddressLine1: options.patient.postalAddressLine1,
      postalAddressLine2: options.patient.postalAddressLine2 ?? null,
      postalAddressLine3: options.patient.postalAddressLine3 ?? null
    });

    this._process = ({
      id: options.process.id,
      rowVersionId: options.process.rowVersionId,
      entityId: options.process.entityId,
      number: options.process.number,
      creation: options.process.creation
    });

    this._form = this._fb.group({
      patient: this._fb.group({
        number: this._fb.control(this._patient.number, [ Validators.required, AppValidators.patientNumber ]),
        taxNumber: this._fb.control(this._patient.taxNumber, [ Validators.required, AppValidators.taxNumber ]),
        name: this._fb.control(this._patient.name, [ Validators.required, Validators.maxLength(256) ]),
        birth: this._fb.control(this._patient.birth, [ Validators.required ]),
        death: this._fb.control(this._patient.death, [ ]),
        faxNumber: this._fb.control(this._patient.faxNumber, [ Validators.maxLength(16) ]),
        mobileNumber: this._fb.control(this._patient.mobileNumber, [ Validators.maxLength(16) ]),
        phoneNumber: this._fb.control(this._patient.phoneNumber, [ Validators.maxLength(16) ]),
        emailAddress: this._fb.control(this._patient.emailAddress, [ Validators.email ]),
        postalAddressArea: this._fb.control(this._patient.postalAddressArea, [ Validators.required, Validators.maxLength(64) ]),
        postalAddressAreaCode: this._fb.control(this._patient.postalAddressAreaCode, [ Validators.required, Validators.maxLength(16) ]),
        postalAddressLine1: this._fb.control(this._patient.postalAddressLine1, [ Validators.required, Validators.maxLength(256) ]),
        postalAddressLine2: this._fb.control(this._patient.postalAddressLine2, [ Validators.maxLength(256) ]),
        postalAddressLine3: this._fb.control(this._patient.postalAddressLine3, [ Validators.maxLength(256) ])
      }),
      familyIncomeStatement: this._fb.group({
        year: this._fb.control(null as number | null, [ Validators.required ]),
        accessCode: this._fb.control('', [ Validators.required ]),
        memberCount: this._fb.control(null as number | null, [ Validators.required ]),
        amount: this._fb.control(null as number | null, [ Validators.required ])
      }),
      legalRepresentativeSearch: this._fb.group({
        taxNumber: this._fb.control('', [ Validators.required, AppValidators.taxNumber ]),
      }),
      legalRepresentative: this._fb.group({
        taxNumber: this._fb.control('', [ Validators.required, AppValidators.taxNumber ]),
        name: this._fb.control('', [ Validators.required ]),
        faxNumber: this._fb.control('', [ Validators.maxLength(16) ]),
        mobileNumber: this._fb.control('', [ Validators.maxLength(16) ]),
        phoneNumber: this._fb.control('', [ Validators.maxLength(16) ]),
        emailAddress: this._fb.control('', [ Validators.email ]),
        postalAddressArea: this._fb.control('', [ Validators.required, Validators.maxLength(64) ]),
        postalAddressAreaCode: this._fb.control('', [ Validators.required, Validators.maxLength(16) ]),
        postalAddressLine1: this._fb.control('', [ Validators.required, Validators.maxLength(256) ]),
        postalAddressLine2: this._fb.control('', [ Validators.maxLength(256) ]),
        postalAddressLine3: this._fb.control('', [ Validators.maxLength(256) ])
      }),
      payment: this._fb.group({
        method: this._fb.control(null as ProcessPaymentMethod | null, [ Validators.required ]),
        recipient: this._fb.control(null as ProcessPaymentRecipient | null, [ Validators.required ]),
        wireTransfer: this._fb.group({
          iban: this._fb.control('', [ Validators.required ]),
          swiftCode: this._fb.control('', [ Validators.required ])
        })
      }),
      unemploymentStatement: this._fb.group({
        issue: this._fb.control('', [ Validators.required ])
      })
    });

    this._seenSteps = new Set();
    this._step = null;

    this._form.controls.patient.controls.number.disable();
    this._form.controls.patient.controls.taxNumber.disable();
    this._form.controls.patient.controls.name.disable();

    const submitTaskId = `${MainProcessCaptureFeature.name}.submit`;
    const submitTaskMessage = "A guardar alterações...";

    this._form.controls.patient.valueChanges
      .pipe(filter(() => this._form.controls.patient.enabled))
      .pipe(filter(() => this._form.controls.patient.valid))
      .pipe(debounceTime(APP_AUTO_SAVE_DEBOUNCE_TIME))
      .pipe(filter(() => this._form.controls.patient.pristine === false))
      .subscribe((values) => this._submitPatientAutoSave(values));

    this._form.controls.legalRepresentativeSearch.valueChanges
      .pipe(filter(() => this._form.controls.legalRepresentativeSearch.enabled))
      .pipe(filter(() => this._form.controls.legalRepresentativeSearch.valid))
      .subscribe((values) => this._searchLegalRepresentative(values));

    this._form.controls.legalRepresentative.valueChanges
      .pipe(filter(() => this._form.controls.legalRepresentative.enabled))
      .pipe(filter(() => this._form.controls.legalRepresentative.valid))
      .pipe(map(() => this._form.controls.legalRepresentative.getRawValue()))
      .pipe(tap(() => this._taskOverlay.enqueue(submitTaskId, submitTaskMessage)))
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME))
      .pipe(mergeMap((values) => this._submitLegalRepresentative(values)))
      .pipe(tap(() => this._taskOverlay.dequeue(submitTaskId)))
      .subscribe(() => {});
  }

  public removeLegalRepresentative(): void {

    this._form.controls.legalRepresentativeSearch.reset({}, __EEF);

    this._form.controls.legalRepresentative.disable(__EEF);
    this._form.controls.legalRepresentativeSearch.enable(__EEF);
  }

  public async submitPatient(): Promise<void> {

    if (this._form.controls.patient.disabled)
      return;

    if (this._form.controls.patient.invalid)
      return;

    await this._loadingOverlay.enqueueWhile(() =>
      this._submitPatient(this._form.controls.patient.value)
    );
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _loadingOverlay: CoreLoadingOverlayFeature,
    private readonly _processCaptureFeatureClient: MainProcessCaptureFeatureClient,
    private readonly _taskOverlay: CoreTaskOverlayFeature
  ) {}

  private async _searchLegalRepresentative(values: Partial<{
    taxNumber: string | null;
  }>): Promise<void> {

    const response = await this._processCaptureFeatureClient.searchLegalRepresentative({
      taxNumber: values.taxNumber!
    });

    this._form.controls.legalRepresentativeSearch.disable(__EEF);

    this._form.controls.legalRepresentative.enable(__EEF);

    this._form.controls.legalRepresentative.setValue({
      taxNumber: response.legalRepresentative?.taxNumber ?? values.taxNumber!,
      name: response.legalRepresentative?.name ?? null,
      emailAddress: response.legalRepresentative?.emailAddress ?? null,
      faxNumber: response.legalRepresentative?.faxNumber ?? null,
      mobileNumber: response.legalRepresentative?.mobileNumber ?? null,
      phoneNumber: response.legalRepresentative?.phoneNumber ?? null,
      postalAddressArea: response.legalRepresentative?.postalAddressArea ?? null,
      postalAddressAreaCode: response.legalRepresentative?.postalAddressAreaCode ?? null,
      postalAddressLine1: response.legalRepresentative?.postalAddressLine1 ?? null,
      postalAddressLine2: response.legalRepresentative?.postalAddressLine2 ?? null,
      postalAddressLine3: response.legalRepresentative?.postalAddressLine3 ?? null
    }, __EEF);

    const { taxNumber, name } = this._form.controls.legalRepresentative.controls;

    taxNumber.disable();

    if (response.legalRepresentative)
      name.disable();

    this._legalRepresentative = (response.legalRepresentative ?? null) && ({
      id: response.legalRepresentative!.id,
      rowVersionId: response.legalRepresentative!.rowVersionId,
      taxNumber: response.legalRepresentative!.taxNumber,
      name: response.legalRepresentative!.name,
      faxNumber: response.legalRepresentative!.faxNumber ?? null,
      mobileNumber: response.legalRepresentative!.mobileNumber ?? null,
      phoneNumber: response.legalRepresentative!.phoneNumber ?? null,
      emailAddress: response.legalRepresentative!.emailAddress ?? null,
      postalAddressArea: response.legalRepresentative!.postalAddressArea,
      postalAddressAreaCode: response.legalRepresentative!.postalAddressAreaCode,
      postalAddressLine1: response.legalRepresentative!.postalAddressLine1,
      postalAddressLine2: response.legalRepresentative!.postalAddressLine2 ?? null,
      postalAddressLine3: response.legalRepresentative!.postalAddressLine3 ?? null
    });
  }

  private async _submitLegalRepresentative(values: Partial<{
    taxNumber: string | null;
    name: string | null;
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
    emailAddress: string | null;
    postalAddressArea: string | null;
    postalAddressAreaCode: string | null;
    postalAddressLine1: string | null;
    postalAddressLine2: string | null;
    postalAddressLine3: string | null;
  }>): Promise<void> {

    const response = await this._processCaptureFeatureClient.submitLegalRepresentative({
      processId: this._process.id,
      processRowVersionId: this._process.rowVersionId,
      patientRowVersionId: this._patient.rowVersionId,
      legalRepresentativeId: this._legalRepresentative?.id ?? undefined,
      legalRepresentativeRowVersionId: this._legalRepresentative?.rowVersionId ?? undefined,
      taxNumber: this._legalRepresentative
        ? undefined
        : values.taxNumber!,
      name: this._legalRepresentative
        ? undefined
        : values.name!,
      faxNumber: values.faxNumber || undefined,
      mobileNumber: values.mobileNumber || undefined,
      phoneNumber: values.phoneNumber || undefined,
      emailAddress: values.emailAddress || undefined,
      postalAddressArea: values.postalAddressArea!,
      postalAddressAreaCode: values.postalAddressAreaCode!,
      postalAddressLine1: values.postalAddressLine1!,
      postalAddressLine2: values.postalAddressLine2 || undefined,
      postalAddressLine3: values.postalAddressLine3 || undefined,
    });

    this._legalRepresentative = ({
      ...(this._legalRepresentative ?? {
        id: response.legalRepresentativeId!,
        taxNumber: values.taxNumber!,
        name: values.name!,
      }),
      rowVersionId: response.legalRepresentativeRowVersionId,
      faxNumber: values.faxNumber || null,
      mobileNumber: values.mobileNumber || null,
      phoneNumber: values.phoneNumber || null,
      emailAddress: values.emailAddress || null,
      postalAddressArea: values.postalAddressArea!,
      postalAddressAreaCode: values.postalAddressAreaCode!,
      postalAddressLine1: values.postalAddressLine1!,
      postalAddressLine2: values.postalAddressLine2 || null,
      postalAddressLine3: values.postalAddressLine3 || null
    });

    this._patient = ({
      ...this._patient,
      rowVersionId: response.patientRowVersionId
    });

    this._process = ({
      ...this._process,
      rowVersionId: response.processRowVersionId
    });
  }

  private async _submitPatient(values: Partial<{
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
    emailAddress: string | null;
    postalAddressArea: string | null;
    postalAddressAreaCode: string | null;
    postalAddressLine1: string | null;
    postalAddressLine2: string | null;
    postalAddressLine3: string | null;
  }>): Promise<void> {

    const response = await this._processCaptureFeatureClient.submitPatient({
      processId: this._process.id,
      processRowVersionId: this._process.rowVersionId,
      patientId: this._patient.id,
      patientRowVersionId: this._patient.rowVersionId,
      faxNumber: values.faxNumber || undefined,
      mobileNumber: values.mobileNumber || undefined,
      phoneNumber: values.phoneNumber || undefined,
      emailAddress: values.emailAddress || undefined,
      postalAddressArea: values.postalAddressArea!,
      postalAddressAreaCode: values.postalAddressAreaCode!,
      postalAddressLine1: values.postalAddressLine1!,
      postalAddressLine2: values.postalAddressLine2 || undefined,
      postalAddressLine3: values.postalAddressLine3 || undefined
    });

    this._patient.faxNumber = values.faxNumber || null;
    this._patient.mobileNumber = values.mobileNumber || null;
    this._patient.phoneNumber = values.phoneNumber || null;
    this._patient.emailAddress = values.emailAddress || null;
    this._patient.postalAddressArea = values.postalAddressArea!;
    this._patient.postalAddressAreaCode = values.postalAddressAreaCode!;
    this._patient.postalAddressLine1 = values.postalAddressLine1!;
    this._patient.postalAddressLine2 = values.postalAddressLine2 || null;
    this._patient.postalAddressLine3 = values.postalAddressLine3 || null;

    this._process.rowVersionId = response.processRowVersionId;
    this._patient.rowVersionId = response.patientRowVersionId;

    if (isSameObject(this._form.controls.patient.value, values))
      this._form.controls.patient.markAsPristine();
  }

  private async _submitPatientAutoSave(values: Partial<{
    faxNumber: string | null;
    mobileNumber: string | null;
    phoneNumber: string | null;
    emailAddress: string | null;
    postalAddressArea: string | null;
    postalAddressAreaCode: string | null;
    postalAddressLine1: string | null;
    postalAddressLine2: string | null;
    postalAddressLine3: string | null;
  }>): Promise<void> {
    await this._taskOverlay.enqueueWhile(
      'A guardar alterações...',
      () => this._submitPatient(values)
    );
  }

}
