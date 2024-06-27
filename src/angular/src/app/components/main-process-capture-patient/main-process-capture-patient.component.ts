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

  // #region State

  @ViewChild('notice')
  private _notice: SuiModalComponent = undefined!;

  // #endregion

  // #region State accessors

  public get form(): MainProcessCaptureFeatureForm {
    return this._processCapture.form;
  }

  // #endregion

  // #region State actions

  public setPatientEnabled(enabled: boolean): void {

    this._processCapture.setPatientEnabled(enabled);

    if (enabled)
      this._notice.open();
  }

  public submitPatient(): Promise<void> {
    return this._processCapture.submitPatient();
  }

  // #endregion

  constructor(
    private readonly _processCapture: MainProcessCaptureFeature
  ) {}
}
