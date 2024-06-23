import { Component } from "@angular/core";
import { MainProcessCaptureFeatureForm } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

@Component({
  selector: "app-main-process-capture-confirmation",
  standalone: true,
  templateUrl: "main-process-capture-confirmation.component.html"
})
export class MainProcessCaptureConfirmationComponent {

  // #region State accessors

  public get form(): MainProcessCaptureFeatureForm {
    return this._processCaptureFeature.form;
  }

  // #endregion

  constructor(
    private readonly _processCaptureFeature: MainProcessCaptureFeature
  ) {}
}
