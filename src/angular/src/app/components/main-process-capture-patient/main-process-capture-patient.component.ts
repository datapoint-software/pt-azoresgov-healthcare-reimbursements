import { Component, ViewChild } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { SuiModalComponent } from "@app/components/sui-modal/sui-modal.component";
import { MainProcessCaptureFeatureForm } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

@Component({
  imports: [ ReactiveFormsModule, SuiFormGroupComponent, SuiModalComponent ],
  selector: 'app-main-process-capture-patient',
  standalone: true,
  templateUrl: 'main-process-capture-patient.component.html'
})
export class MainProcessCapturePatientComponent {

  // #region State accessors

  public get form(): MainProcessCaptureFeatureForm {
    return this._processCapture.form;
  }

  // #endregion

  constructor(
    private readonly _processCapture: MainProcessCaptureFeature
  ) {}
}
