import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCaptureFeatureClient } from "@app/api/main-process-capture-feature/main-process-capture-feature.client";
import { APP_INPUT_DEBOUNCE_TIME } from "@app/constants";
import { ProcessPaymentMethod, ProcessPaymentRecipient } from "@app/enums";
import { CoreTaskOverlayFeature } from "@app/features/core-task-overlay/core-task-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCaptureFeatureEntity, MainProcessCaptureFeatureForm, MainProcessCaptureFeaturePatient, MainProcessCaptureFeatureProcess, MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { AppValidators } from "@app/validators";
import { debounceTime, filter, map, mergeMap, tap } from "rxjs";

const EMIT_EVENT_FALSE = { emitEvent: false };

@Injectable()
export class MainProcessCaptureFeature implements Feature {

  // #region State

  private _entities: Map<string, MainProcessCaptureFeatureEntity> = undefined!;

  private _form: MainProcessCaptureFeatureForm = undefined!;

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
        identity: this._fb.group({
          number: this._fb.control(this._patient.number, [ Validators.required, AppValidators.patientNumber ]),
          taxNumber: this._fb.control(this._patient.taxNumber, [ Validators.required, AppValidators.taxNumber ]),
          name: this._fb.control(this._patient.name, [ Validators.required, Validators.maxLength(256) ]),
          birth: this._fb.control(this._patient.birth, [ Validators.required ]),
          death: this._fb.control(this._patient.death, [ ]),
        }),
        contacts: this._fb.group({
          faxNumber: this._fb.control(this._patient.faxNumber, [ Validators.maxLength(16) ]),
          mobileNumber: this._fb.control(this._patient.mobileNumber, [ Validators.maxLength(16) ]),
          phoneNumber: this._fb.control(this._patient.phoneNumber, [ Validators.maxLength(16) ]),
          emailAddress: this._fb.control(this._patient.emailAddress, [ Validators.email ]),
        }),
        postalAddress: this._fb.group({
          postalAddressArea: this._fb.control(this._patient.postalAddressArea, [ Validators.required, Validators.maxLength(64) ]),
          postalAddressAreaCode: this._fb.control(this._patient.postalAddressAreaCode, [ Validators.required, Validators.maxLength(16) ]),
          postalAddressLine1: this._fb.control(this._patient.postalAddressLine1, [ Validators.required, Validators.maxLength(256) ]),
          postalAddressLine2: this._fb.control(this._patient.postalAddressLine2, [ Validators.maxLength(256) ]),
          postalAddressLine3: this._fb.control(this._patient.postalAddressLine3, [ Validators.maxLength(256) ])
        })
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
        identity: this._fb.group({
          taxNumber: this._fb.control('', [ Validators.required, AppValidators.taxNumber ]),
          name: this._fb.control('', [ Validators.required ]),
        }),
        contacts: this._fb.group({
          faxNumber: this._fb.control('', [ Validators.maxLength(16) ]),
          mobileNumber: this._fb.control('', [ Validators.maxLength(16) ]),
          phoneNumber: this._fb.control('', [ Validators.maxLength(16) ]),
          emailAddress: this._fb.control('', [ Validators.email ]),
        }),
        postalAddress: this._fb.group({
          postalAddressArea: this._fb.control('', [ Validators.required, Validators.maxLength(64) ]),
          postalAddressAreaCode: this._fb.control('', [ Validators.required, Validators.maxLength(16) ]),
          postalAddressLine1: this._fb.control('', [ Validators.required, Validators.maxLength(256) ]),
          postalAddressLine2: this._fb.control('', [ Validators.maxLength(256) ]),
          postalAddressLine3: this._fb.control('', [ Validators.maxLength(256) ])
        })
      }),
      payment: this._fb.group({
        method: this._fb.control(null as ProcessPaymentMethod | null, [ Validators.required ]),
        recipient: this._fb.control(null as ProcessPaymentRecipient | null, [ Validators.required ]),
        wireTransferDetails: this._fb.group({
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

    this._form.controls.legalRepresentative.disable();

    this._form.controls.patient.disable();
    this._form.controls.patient.controls.identity.disable();

    const submitTaskId = `${MainProcessCaptureFeature.name}.submit`;
    const submitTaskMessage = "A guardar alterações...";

    this._form.controls.patient.valueChanges
      .pipe(filter(() => this._form.controls.patient.enabled))
      .pipe(filter(() => this._form.controls.patient.valid))
      .pipe(tap(() => this._taskOverlay.enqueue(submitTaskId, submitTaskMessage)))
      .pipe(debounceTime(APP_INPUT_DEBOUNCE_TIME * 10))
      .pipe(mergeMap((values) => this._submitPatient(values)))
      .pipe(tap(() => this._taskOverlay.dequeue(submitTaskId)))
      .subscribe(() => {});

    this._form.controls.legalRepresentativeSearch.valueChanges
      .pipe(filter(() => this._form.controls.legalRepresentativeSearch.enabled))
      .pipe(filter(() => this._form.controls.legalRepresentativeSearch.valid))
      .subscribe((values) => this._searchLegalRepresentative(values));
  }

  public removeLegalRepresentative(): void {

    this._form.controls.legalRepresentativeSearch.reset({}, EMIT_EVENT_FALSE);

    this._form.controls.legalRepresentative.disable(EMIT_EVENT_FALSE);
    this._form.controls.legalRepresentativeSearch.enable(EMIT_EVENT_FALSE);
  }

  public setPatientEnabled(enabled: boolean): void {

    const { patient } = this._form.controls;

    enabled
      ? patient.enable(EMIT_EVENT_FALSE)
      : patient.disable(EMIT_EVENT_FALSE);

    if (enabled)
      patient.controls.identity.disable(EMIT_EVENT_FALSE);
  }

  public async submitPatient(): Promise<void> {

    if (this._form.controls.patient.disabled)
      return;

    if (this._form.controls.patient.invalid)
      return;

    await this._submitPatient(
      this._form.controls.patient.value
    );
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _processCaptureFeatureClient: MainProcessCaptureFeatureClient,
    private readonly _taskOverlay: CoreTaskOverlayFeature
  ) {}

  private async _searchLegalRepresentative(values: Partial<{
    taxNumber: string | null;
  }>): Promise<void> {

    const response = await this._processCaptureFeatureClient.searchLegalRepresentative({
      taxNumber: values.taxNumber!
    });

    this._form.controls.legalRepresentativeSearch.disable(EMIT_EVENT_FALSE);

    this._form.controls.legalRepresentative.enable(EMIT_EVENT_FALSE);

    this._form.controls.legalRepresentative.setValue({
      identity: {
        taxNumber: response.legalRepresentative?.taxNumber ?? values.taxNumber!,
        name: response.legalRepresentative?.name ?? null,
      },
      contacts: {
        emailAddress: response.legalRepresentative?.emailAddress ?? null,
        faxNumber: response.legalRepresentative?.faxNumber ?? null,
        mobileNumber: response.legalRepresentative?.mobileNumber ?? null,
        phoneNumber: response.legalRepresentative?.phoneNumber ?? null
      },
      postalAddress: {
        postalAddressArea: response.legalRepresentative?.postalAddressArea ?? null,
        postalAddressAreaCode: response.legalRepresentative?.postalAddressAreaCode ?? null,
        postalAddressLine1: response.legalRepresentative?.postalAddressLine1 ?? null,
        postalAddressLine2: response.legalRepresentative?.postalAddressLine2 ?? null,
        postalAddressLine3: response.legalRepresentative?.postalAddressLine3 ?? null
      }
    }, EMIT_EVENT_FALSE);

    if (response.legalRepresentative)
      this._form.controls.legalRepresentative.controls.identity.disable(EMIT_EVENT_FALSE);
    else
      this._form.controls.legalRepresentative.controls.identity.controls.taxNumber.disable(EMIT_EVENT_FALSE);
  }

  private async _submitPatient(values: Partial<{
    contacts: Partial<{
      faxNumber: string | null;
      mobileNumber: string | null;
      phoneNumber: string | null;
      emailAddress: string | null;
    }>;
    postalAddress: Partial<{
      postalAddressArea: string | null;
      postalAddressAreaCode: string | null;
      postalAddressLine1: string | null;
      postalAddressLine2: string | null;
      postalAddressLine3: string | null;
    }>;
  }>): Promise<void> {

    const response = await this._processCaptureFeatureClient.submitPatient({
      processId: this._process.id,
      processRowVersionId: this._process.rowVersionId,
      patientId: this._patient.id,
      patientRowVersionId: this._patient.rowVersionId,
      faxNumber: values.contacts?.faxNumber || undefined,
      mobileNumber: values.contacts?.mobileNumber || undefined,
      phoneNumber: values.contacts?.phoneNumber || undefined,
      emailAddress: values.contacts?.emailAddress || undefined,
      postalAddressArea: values.postalAddress!.postalAddressArea!,
      postalAddressAreaCode: values.postalAddress!.postalAddressAreaCode!,
      postalAddressLine1: values.postalAddress!.postalAddressLine1!,
      postalAddressLine2: values.postalAddress!.postalAddressLine2 || undefined,
      postalAddressLine3: values.postalAddress!.postalAddressLine3 || undefined
    });

    this._patient.faxNumber = values.contacts?.faxNumber || null;
    this._patient.mobileNumber = values.contacts?.mobileNumber || null;
    this._patient.phoneNumber = values.contacts?.phoneNumber || null;
    this._patient.emailAddress = values.contacts?.emailAddress || null;
    this._patient.postalAddressArea = values.postalAddress!.postalAddressArea!;
    this._patient.postalAddressAreaCode = values.postalAddress!.postalAddressAreaCode!;
    this._patient.postalAddressLine1 = values.postalAddress!.postalAddressLine1!;
    this._patient.postalAddressLine2 = values.postalAddress!.postalAddressLine2 || null;
    this._patient.postalAddressLine3 = values.postalAddress!.postalAddressLine3 || null;

    this._process.rowVersionId = response.processRowVersionId;
    this._patient.rowVersionId = response.patientRowVersionId;
  }

}
