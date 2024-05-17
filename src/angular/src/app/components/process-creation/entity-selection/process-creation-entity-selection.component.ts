import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessCreationEntitySelectionFeatureEntity, ProcessCreationEntitySelectionFeatureForm, ProcessCreationEntitySelectionFeatureSearchResult } from "../../../features/process-creation/entity-selection/process-creation-entity-selection-feature.abstractions";
import { ProcessCreationEntitySelectionFeature } from "../../../features/process-creation/entity-selection/process-creation-entity-selection.feature";
import { FormGroupComponent } from "../../form-group/form-group.component";
import { ProcessCreationEntitySelectionComponentSearchResultEntity } from "./process-creation-entity-selection-component.abstractions";
import { Router } from "@angular/router";

@Component({
  imports: [ FormGroupComponent, ReactiveFormsModule ],
  selector: 'app-process-creation-entity-selection',
  standalone: true,
  templateUrl: './process-creation-entity-selection.component.html'
})
export class ProcessCreationEntitySelectionComponent {

  // #region State accessors

  public get entities(): ReadonlyMap<string, Readonly<ProcessCreationEntitySelectionFeatureEntity>> {
    return this._processCreationEntitySelection.entities;
  }

  public get entity(): ProcessCreationEntitySelectionFeatureEntity | null {
    return this._processCreationEntitySelection.entity;
  }

  public get form(): ProcessCreationEntitySelectionFeatureForm {
    return this._processCreationEntitySelection.form;
  }

  public get searchResult(): ProcessCreationEntitySelectionFeatureSearchResult | null {
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
    private readonly _processCreationEntitySelection: ProcessCreationEntitySelectionFeature,
    private readonly _router: Router
  ) {}
}
