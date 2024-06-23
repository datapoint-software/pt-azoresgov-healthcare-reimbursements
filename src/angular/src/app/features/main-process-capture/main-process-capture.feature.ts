import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { MainProcessCaptureFeatureClient } from "@app/api/main-process-capture-feature/main-process-capture-feature.client";
import { ProcessPaymentMethod, ProcessPaymentRecipient } from "@app/enums";
import { Feature } from "@app/features/feature.abstractions";
import { MainProcessCaptureFeatureEntity, MainProcessCaptureFeatureForm, MainProcessCaptureFeaturePatient, MainProcessCaptureFeatureProcess, MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { AppValidators } from "@app/validators";

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
        filter: this._fb.control('', [ Validators.required, AppValidators.taxNumber ]),
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

    const patient = this._form.controls.patient;
    const legalRepresentative = this._form.controls.legalRepresentative;

    // The following controls are disabled.
    patient.controls.identity.disable();
    legalRepresentative.controls.identity.disable();
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _processCaptureFeatureClient: MainProcessCaptureFeatureClient
  ) {}

}
