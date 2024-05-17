import { Component } from "@angular/core";
import { Router, RouterOutlet } from "@angular/router";
import { ProcessCreationFeatureStep } from "../../features/process-creation/process-creation-feature.abstractions";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";
import { ProcessCreationNavigationComponent } from "./navigation/process-creation-navigation.component";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation/entity-selection/process-creation-entity-selection.feature";
import { ProcessCreationPatientSelectionFeature } from "../../features/process-creation/patient-selection/process-creation-patient-selection.feature";

@Component({
  imports: [ ProcessCreationNavigationComponent, RouterOutlet ],
  selector: 'app-process-creation',
  standalone: true,
  templateUrl: './process-creation.component.html'
})
export class ProcessCreationComponent {

  // #region State accessors

  public get confirmationStep(): boolean {
    return this._processCreation.step === ProcessCreationFeatureStep.Confirmation;
  }

  public get entitySelectionStep(): boolean {
    return this._processCreation.step === ProcessCreationFeatureStep.EntitySelection;
  }

  public get firstStep(): boolean {
    return this._processCreation.stepNumber === 1;
  }

  public get lastStep(): boolean {
    return this._processCreation.stepNumber === this._processCreation.stepCount;
  }

  public get incomplete(): boolean {
    switch (this._processCreation.step) {
      case ProcessCreationFeatureStep.EntitySelection:
        return !this._processCreationEntitySelection.complete;
      case ProcessCreationFeatureStep.PatientSelection:
        return !this._processCreationPatientSelection.complete;
      default:
        return true;
    }
  }

  public get patientSelectionStep(): boolean {
    return this._processCreation.step === ProcessCreationFeatureStep.PatientSelection;
  }

  public get stepCount(): number {
    return this._processCreation.stepCount;
  }

  public get stepNumber(): number {
    return this._processCreation.stepNumber;
  }

  // #endregion

  // #region Actions

  public next(): void {
    this._navigate(this._processCreation.nextStep!);
  }

  public previous(): void {
    this._navigate(this._processCreation.previousStep!);
  }

  // #endregion

  private _navigate(step: ProcessCreationFeatureStep): void {

    const token = (
      step === ProcessCreationFeatureStep.EntitySelection ? 'entity' :
      step === ProcessCreationFeatureStep.PatientSelection ? 'patient' :
      'confirmation'
    );

    this._router.navigate([ '/processes', '_', token ]);
  }

  constructor(
    private readonly _processCreation: ProcessCreationFeature,
    private readonly _processCreationEntitySelection: ProcessCreationEntitySelectionFeature,
    private readonly _processCreationPatientSelection: ProcessCreationPatientSelectionFeature,
    private readonly _router: Router
  ) {}
}
