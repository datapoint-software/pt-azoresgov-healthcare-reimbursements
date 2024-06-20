import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";
import { MainProcessCreationFeatureEntity, MainProcessCreationFeatureEntitySearchForm, MainProcessCreationFeatureEntitySearchResult } from "@app/features/main-process-creation/main-process-creation.feature.abstractions";

@Component({
  imports: [ ReactiveFormsModule, SuiFormGroupComponent ],
  selector: 'app-main-process-creation-entity-selection',
  standalone: true,
  templateUrl: 'main-process-creation-entity-selection.component.html'
})
export class MainProcessCreationEntitySelectionComponent {

  // #region State accessors

  public get entities(): ReadonlyMap<string, MainProcessCreationFeatureEntity> {
    return this._processCreation.entities;
  }

  public get entity(): Readonly<MainProcessCreationFeatureEntity> | null {
    return this._processCreation.entity;
  }

  public get entitySearchForm(): MainProcessCreationFeatureEntitySearchForm {
    return this._processCreation.entitySearchForm;
  }

  public get entitySearchResult(): MainProcessCreationFeatureEntitySearchResult | null {
    return this._processCreation.entitySearchResult;
  }

  // #endregion

  // #region Actions

  public searchEntities(): Promise<void> {
    return this._processCreation.searchEntities();
  }

  public selectEntity(entityId: string): void {
    this._processCreation.selectEntity(entityId);
    this._router.navigate([ '/processes', '_', 'patient-selection' ]);
  }

  // #endregion

  constructor(
    private readonly _processCreation: MainProcessCreationFeature,
    private readonly _router: Router
  ) {}

}
