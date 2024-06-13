import { Component } from "@angular/core";
import { Router, RouterOutlet } from "@angular/router";
import { MainProcessCreationNavigationComponent } from "@app/components/main-process-creation-navigation/main-process-creation-navigation.component";
import { MainProcessCreationEntitySelectionFeatureEntity } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection-feature.abstractions";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation-feature.abstractions";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

@Component({
  imports: [ MainProcessCreationNavigationComponent, RouterOutlet ],
  selector: 'app-main-process-creation',
  standalone: true,
  templateUrl: 'main-process-creation.component.html'
})
export class MainProcessCreationComponent {

  // #region State accessors

  public get confirmationStep(): boolean {
    return this._processCreation.step === MainProcessCreationFeatureStep.Confirmation;
  }

  public get entity(): MainProcessCreationEntitySelectionFeatureEntity | null {
    return this._processCreationEntitySelection.entity;
  }

  public get entitySelectionStep(): boolean {
    return this._processCreation.step === MainProcessCreationFeatureStep.EntitySelection;
  }

  public get firstStep(): boolean {
    return this._processCreation.stepNumber === 1;
  }

  public get lastStep(): boolean {
    return this._processCreation.stepNumber === this._processCreation.stepCount;
  }

  public get incomplete(): boolean {
    switch (this._processCreation.step) {
      case MainProcessCreationFeatureStep.EntitySelection:
        return !this._processCreationEntitySelection.complete;
      case MainProcessCreationFeatureStep.PatientSelection:
        return !this._processCreationPatientSelection.complete;
      default:
        return true;
    }
  }

  public get patientSelectionStep(): boolean {
    return this._processCreation.step === MainProcessCreationFeatureStep.PatientSelection;
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

  private _navigate(step: MainProcessCreationFeatureStep): void {

    const token = (
      step === MainProcessCreationFeatureStep.EntitySelection ? 'entity' :
      step === MainProcessCreationFeatureStep.PatientSelection ? 'patient' :
      'confirmation'
    );

    this._router.navigate([ '/processes', '_', token ]);
  }

  constructor(
    private readonly _processCreation: MainProcessCreationFeature,
    private readonly _processCreationEntitySelection: MainProcessCreationEntitySelectionFeature,
    private readonly _processCreationPatientSelection: MainProcessCreationPatientSelectionFeature,
    private readonly _router: Router
  ) {}
}
