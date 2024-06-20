import { Component, OnInit } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessSearchFeatureEntity, MainProcessSearchFeaturePatient, MainProcessSearchFeatureProcess, MainProcessSearchFeatureProcessSearchForm, MainProcessSearchFeatureProcessSearchResult } from "@app/features/main-process-search/main-process-search-feature.abstractions";
import { MainProcessSearchFeature } from "@app/features/main-process-search/main-process-search.feature";
import { NumericPipe } from "@app/pipes/numeric/numeric.pipe";

@Component({
  imports: [ NumericPipe, ReactiveFormsModule, SuiFormGroupComponent ],
  selector: 'app-main-process-search',
  standalone: true,
  templateUrl: 'main-process-search.component.html'
})
export class MainProcessSearchComponent implements OnInit {

  // #region State accessors

  public get entities(): ReadonlyMap<string, Readonly<MainProcessSearchFeatureEntity>> {
    return this._processSearchFeature.entities;
  }

  public get patients(): ReadonlyMap<string, Readonly<MainProcessSearchFeaturePatient>> {
    return this._processSearchFeature.patients;
  }

  public get processes(): ReadonlyMap<string, Readonly<MainProcessSearchFeatureProcess>> {
    return this._processSearchFeature.processes;
  }

  public get processSearchForm(): MainProcessSearchFeatureProcessSearchForm {
    return this._processSearchFeature.processSearchForm;
  }

  public get processSearchResult(): Readonly<MainProcessSearchFeatureProcessSearchResult> | null {
    return this._processSearchFeature.processSearchResult;
  }

  public get processSearchResultLeftoverMatchCount(): number | null {
    return this._processSearchFeature.processSearchResultLeftoverMatchCount;
  }

  // #endregion

  // #region Actions

  public navigate(processId: string): void {
    this._router.navigate([
      '/processes',
      processId,
      'capture'
    ]);
  }

  public async searchProcesses(): Promise<void> {
    await this._processSearchFeature.searchProcesses();
  }

  // #endregion

  constructor(
    private readonly _processSearchFeature: MainProcessSearchFeature,
    private readonly _router: Router
  ) {}

  ngOnInit(): void {
    this._processSearchFeature.searchProcesses();
  }
}
