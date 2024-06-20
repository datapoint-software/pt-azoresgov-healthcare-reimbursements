import { Component } from "@angular/core";
import { Router, RouterOutlet } from "@angular/router";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureStep } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";

@Component({
  imports: [ RouterOutlet ],
  selector: 'main-process-creation',
  standalone: true,
  templateUrl: 'main-process-creation.component.html'
})
export class MainProcessCreationComponent {

  // #region State accessors

  public get step(): MainProcessCreationFeatureStep {
    return this._processCreationFeature.step;
  }

  public get stepCount(): number {
    return this._processCreationFeature.stepCount;
  }

  public get stepNumber(): number {
    return this._processCreationFeature.stepNumber;
  }

  // #endregion

  // #region Queries

  public isPending(step: MainProcessCreationFeatureStep): boolean {
    return step === MainProcessCreationFeatureStep.EntitySelection ? !this._processCreationFeature.entity :
      true;
  }

  public isConfirmation(step: MainProcessCreationFeatureStep): boolean {
    return step === MainProcessCreationFeatureStep.Confirmation;
  }

  public isEntitySelection(step: MainProcessCreationFeatureStep): boolean {
    return step === MainProcessCreationFeatureStep.EntitySelection;
  }

  public isPatientSelection(step: MainProcessCreationFeatureStep): boolean {
    return step === MainProcessCreationFeatureStep.PatientSelection;
  }

  // #endregion

  // #region Actions

  public next(): void {

    switch (this.step) {
      case MainProcessCreationFeatureStep.EntitySelection:
        this._router.navigate([ '/processes', '_', 'patient-selection' ]);
        return;

      case MainProcessCreationFeatureStep.PatientSelection:
        this._router.navigate([ '/processes', '_', 'confirmation' ]);
        return;
    }
  }

  public previous(): void {

    switch (this.step) {

      case MainProcessCreationFeatureStep.PatientSelection:
        this._router.navigate([ '/processes', '_', 'entity-selection' ]);
        return;

      case MainProcessCreationFeatureStep.Confirmation:
        this._router.navigate([ '/processes', '_', 'patient-selection' ]);
        return;
    }
  }

  // #endregion

  constructor(
    private readonly _processCreationFeature: MainProcessCreationFeature,
    private readonly _router: Router
  ) {}
}
