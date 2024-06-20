import { DatePipe } from "@angular/common";
import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureEntity, MainProcessCreationFeaturePatient, MainProcessCreationFeaturePatientSearchForm, MainProcessCreationFeaturePatientSearchResult } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";
import { NumericPipe } from "@app/pipes/numeric/numeric.pipe";

@Component({
  imports: [ DatePipe, NumericPipe, ReactiveFormsModule, SuiFormGroupComponent ],
  selector: 'app-main-process-creation-patient-selection',
  standalone: true,
  templateUrl: 'main-process-creation-patient-selection.component.html'
})
export class MainProcessCreationPatientSelectionComponent {

  // #region State accessors

  public get entities(): ReadonlyMap<string, MainProcessCreationFeatureEntity> {
    return this._processCreationFeature.entities;
  }

  public get entity(): Readonly<MainProcessCreationFeatureEntity> {
    return this._processCreationFeature.entity!;
  }

  public get patient(): Readonly<MainProcessCreationFeaturePatient> | null {
    return this._processCreationFeature.patient;
  }

  public get patients(): ReadonlyMap<string, MainProcessCreationFeaturePatient> {
    return this._processCreationFeature.patients;
  }

  public get patientSearchForm(): MainProcessCreationFeaturePatientSearchForm {
    return this._processCreationFeature.patientSearchForm;
  }

  public get patientSearchResult(): Readonly<MainProcessCreationFeaturePatientSearchResult> | null {
    return this._processCreationFeature.patientSearchResult;
  }

  // #endregion

  // #region Actions

  public async searchPatients(): Promise<void> {
    await this._processCreationFeature.searchPatients();
  }

  public selectPatient(patientId: string): void {
    this._processCreationFeature.selectPatient(patientId);
    this._router.navigate([ '/processes', '_', 'confirmation' ]);
  }

  // #endregion

  // #region State queries

  public isPatientSelectable(patientId: string): boolean {
    return this._processCreationFeature.isPatientSelectable(patientId);
  }

  // #endregion

  constructor(
    private readonly _processCreationFeature: MainProcessCreationFeature,
    private readonly _router: Router
  ) {}
}
