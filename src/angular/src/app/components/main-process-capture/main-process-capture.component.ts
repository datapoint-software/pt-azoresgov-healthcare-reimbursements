import { Component, ViewChild } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { RouterLink, RouterOutlet } from "@angular/router";
import { SuiModalComponent } from "@app/components/sui-modal/sui-modal.component";
import { MainProcessCaptureFeatureForm, MainProcessCaptureFeatureProcess, MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

@Component({
  imports: [ ReactiveFormsModule, RouterLink, RouterOutlet, SuiModalComponent ],
  selector: 'app-main-process-capture',
  standalone: true,
  templateUrl: 'main-process-capture.component.html'
})
export class MainProcessCaptureComponent {

  // #region State accessors

  public get complete(): boolean {
    return this._processCaptureFeature.seenSteps.size > 4;
  }

  public get confirmationStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.Confirmation;
  }

  public get confirmationStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.Confirmation);
  }

  public get form(): MainProcessCaptureFeatureForm {
    return this._processCaptureFeature.form;
  }

  public get familyIncomeStatementStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.FamilyIncomeStatement;
  }

  public get familyIncomeStatementStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.FamilyIncomeStatement);
  }

  public get legalRepresentativeStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.LegalRepresentative;
  }

  public get legalRepresentativeStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.LegalRepresentative);
  }

  public get patientStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.Patient;
  }

  public get patientStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.Patient);
  }

  public get paymentStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.Payment;
  }

  public get paymentStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.Payment);
  }

  public get process(): MainProcessCaptureFeatureProcess {
    return this._processCaptureFeature.process;
  }

  public get unemploymentStatementStep(): boolean {
    return this._processCaptureFeature.step === MainProcessCaptureFeatureStep.UnemploymentStatement;
  }

  public get unemploymentStatementStepSeen(): boolean {
    return this._processCaptureFeature.seenSteps.has(MainProcessCaptureFeatureStep.UnemploymentStatement);
  }

  // #endregion

  constructor(
    private readonly _processCaptureFeature: MainProcessCaptureFeature
  ) {}
}
