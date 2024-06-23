import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessCaptureFeatureForm } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

@Component({
  imports: [ ReactiveFormsModule, SuiFormGroupComponent ],
  selector: "app-main-process-capture-payment",
  standalone: true,
  templateUrl: "main-process-capture-payment.component.html"
})
export class MainProcessCapturePaymentComponent {

  // #region State accessors

  public get form(): MainProcessCaptureFeatureForm {
    return this._processCapture.form;
  }

  // #endregion

  constructor(
    private readonly _processCapture: MainProcessCaptureFeature
  ) {}
}
