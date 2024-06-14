import { DatePipe } from "@angular/common";
import { Component } from "@angular/core";
import { MainProcessCreationConfirmationFeatureEntity, MainProcessCreationConfirmationFeaturePatient } from "@app/features/main-process-creation-confirmation/main-process-creation-confirmation-feature.abstractions";
import { MainProcessCreationConfirmationFeature } from "@app/features/main-process-creation-confirmation/main-process-creation-confirmation.feature";
import { EntityNatureLabelPipe } from "@app/pipes/entity-nature-label/entity-nature-label.pipe";
import { NumericPipe } from "@app/pipes/numeric/numeric.pipe";

@Component({
  imports: [ DatePipe, EntityNatureLabelPipe, NumericPipe ],
  selector: 'app-main-process-creation-confirmation',
  standalone: true,
  templateUrl: 'main-process-creation-confirmation.component.html'
})
export class MainProcessCreationConfirmationComponent {

  // #region State accessors

  public get entity(): MainProcessCreationConfirmationFeatureEntity {
    return this._processCreationConfirmationFeature.entity;
  }

  public get patient(): MainProcessCreationConfirmationFeaturePatient {
    return this._processCreationConfirmationFeature.patient;
  }

  // #endregion

  constructor(
    private readonly _processCreationConfirmationFeature: MainProcessCreationConfirmationFeature
  ) {}
}
