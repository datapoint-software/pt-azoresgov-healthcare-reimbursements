import { DatePipe } from "@angular/common";
import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessCreationPatientSelectionFeatureForm, MainProcessCreationPatientSelectionFeaturePatient, MainProcessCreationPatientSelectionFeatureSearchResult } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection-feature.abstractions";
import { MainProcessCreationPatientSelectionFeature } from "@app/features/main-process-creation-patient-selection/main-process-creation-patient-selection.feature";

@Component({
  imports: [ DatePipe, SuiFormGroupComponent, ReactiveFormsModule ],
  selector: 'app-main-process-creation-patient-selection',
  standalone: true,
  templateUrl: 'main-process-creation-patient-selection.component.html'
})
export class MainProcessCreationPatientSelectionComponent {

  // #region State accessors

  public get patients(): ReadonlyMap<string, Readonly<MainProcessCreationPatientSelectionFeaturePatient>> {
    return this._processCreationPatientSelection.patients;
  }

  public get patient(): Readonly<MainProcessCreationPatientSelectionFeaturePatient> | null {
    return this._processCreationPatientSelection.patient;
  }

  public get form(): MainProcessCreationPatientSelectionFeatureForm {
    return this._processCreationPatientSelection.form;
  }

  public get fullSearch(): boolean {
    return !!this._processCreationPatientSelection.form.value.full;
  }

  public get searchResult(): Readonly<MainProcessCreationPatientSelectionFeatureSearchResult> | null {
    return this._processCreationPatientSelection.searchResult;
  }

  public get searchResultPatients(): ReadonlyArray<MainProcessCreationPatientSelectionFeaturePatient> {
    return (this._processCreationPatientSelection.searchResult?.patientIds ?? [])
      .map((patientId) => this._processCreationPatientSelection.patients.get(patientId)!);
  }

  // #endregion

  // #region Actions

  public select(patientId: string): void {
    this._processCreationPatientSelection.select(patientId);
    this._router.navigate([ '/processes', '_', 'confirmation' ]);
  }

  public submit(): void {
    this._processCreationPatientSelection.search();
  }

  // #endregion

  constructor(
    private readonly _processCreationPatientSelection: MainProcessCreationPatientSelectionFeature,
    private readonly _router: Router
  ) {}

}
