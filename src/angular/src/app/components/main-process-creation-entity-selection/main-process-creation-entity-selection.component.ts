import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { Router } from "@angular/router";
import { ProcessCreationEntitySelectionComponentSearchResultEntity } from "@app/components/main-process-creation-entity-selection/main-process-creation-entity-selection-component.abstractions";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { MainProcessCreationEntitySelectionFeatureEntity, MainProcessCreationEntitySelectionFeatureForm, MainProcessCreationEntitySelectionFeatureSearchResult } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection-feature.abstractions";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";

@Component({
  imports: [ SuiFormGroupComponent, ReactiveFormsModule ],
  selector: 'app-main-process-creation-entity-selection',
  standalone: true,
  templateUrl: 'main-process-creation-entity-selection.component.html'
})
export class MainProcessCreationEntitySelectionComponent {

  // #region State accessors

  public get entities(): ReadonlyMap<string, Readonly<MainProcessCreationEntitySelectionFeatureEntity>> {
    return this._processCreationEntitySelection.entities;
  }

  public get entity(): MainProcessCreationEntitySelectionFeatureEntity | null {
    return this._processCreationEntitySelection.entity;
  }

  public get form(): MainProcessCreationEntitySelectionFeatureForm {
    return this._processCreationEntitySelection.form;
  }

  public get searchResult(): MainProcessCreationEntitySelectionFeatureSearchResult | null {
    return this._processCreationEntitySelection.searchResult;
  }

  public get searchResultEntities(): ReadonlyArray<Readonly<ProcessCreationEntitySelectionComponentSearchResultEntity>> {
    return this._processCreationEntitySelection
      .searchResult?.entityIds
      .map((id) => {
        const entity = this._processCreationEntitySelection.entities.get(id)!;
        const parentEntity = (entity.parentEntityId && this._processCreationEntitySelection.entities.get(entity.parentEntityId)) || null;

        return ({
          id: entity.id,
          rowVersionId: entity.rowVersionId,
          parentEntity: parentEntity && ({
            id: parentEntity.id,
            rowVersionId: parentEntity.rowVersionId,
            parentEntity: null,
            code: parentEntity.code,
            name: parentEntity.name,
            nature: parentEntity.nature
          }),
          code: entity.code,
          name: entity.name,
          nature: entity.nature
        })
      })

      ?? [];
  }

  // #endregion

  // #region Actions

  public select(entityId: string): void {
    console.log('entityId: ', entityId);
    this._processCreationEntitySelection.select(entityId);
    this._router.navigate([ '/processes', '_', 'patient' ]);
    console.log('entityId: ', entityId);
  }

  public submit(): void {
    this._processCreationEntitySelection.search();
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelection: MainProcessCreationEntitySelectionFeature,
    private readonly _router: Router
  ) {}
}
