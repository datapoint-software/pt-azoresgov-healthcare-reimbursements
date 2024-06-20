import { DatePipe } from "@angular/common";
import { Component, ViewChild } from "@angular/core";
import { RouterLink } from "@angular/router";
import { SuiModalComponent } from "@app/components/sui-modal/sui-modal.component";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureEntity, MainProcessCreationFeaturePatient, MainProcessCreationFeatureProcess } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";
import { EntityNatureLabelPipe } from "@app/pipes/entity-nature-label/entity-nature-label.pipe";
import { NumericPipe } from "@app/pipes/numeric/numeric.pipe";

@Component({
  imports: [ DatePipe, EntityNatureLabelPipe, NumericPipe, RouterLink, SuiModalComponent ],
  selector: 'app-main-process-creation-confirmation',
  standalone: true,
  templateUrl: 'main-process-creation-confirmation.component.html'
})
export class MainProcessCreationConfirmationComponent {

  // #region State

  @ViewChild("confirmation")
  private _confirmation: SuiModalComponent = undefined!;

  @ViewChild("completion")
  private _completion: SuiModalComponent = undefined!;

  // #endregion

  // #region State accessors

  public get entity(): Readonly<MainProcessCreationFeatureEntity> {
    return this._processCreationFeature.entity!;
  }

  public get parentEntity(): Readonly<MainProcessCreationFeatureEntity> | null {
    return (this.entity?.parentEntityId && this._processCreationFeature.entities.get(this.entity.parentEntityId)!) || null;
  }

  public get patientEntity(): Readonly<MainProcessCreationFeatureEntity> {
    return this._processCreationFeature.entities.get(this._processCreationFeature.patient!.entityId)!;
  }

  public get patientParentEntity(): Readonly<MainProcessCreationFeatureEntity> | null {
    return (this.patientEntity?.parentEntityId && this._processCreationFeature.entities.get(this.patientEntity.parentEntityId)!) || null;
  }

  public get patient(): Readonly<MainProcessCreationFeaturePatient> {
    return this._processCreationFeature.patient!;
  }

  public get process(): Readonly<MainProcessCreationFeatureProcess> | null {
    return this._processCreationFeature.process;
  }

  // #endregion

  // #region Actions

  async confirm(): Promise<void> {

    await this._processCreationFeature.confirm();

    this._confirmation.close();
    this._completion.open();

  }

  // #endregion

  constructor(
    private readonly _processCreationFeature: MainProcessCreationFeature
  ) {}

}
