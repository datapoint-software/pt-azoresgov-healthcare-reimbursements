import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessCreationEntitySelectionFeatureForm } from "../../features/process-creation-entity-selection/process-creation-entity-selection-feature.abstractions";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [ FormGroupComponent, ReactiveFormsModule ],
  selector: 'app-process-creation-entity-selection',
  standalone: true,
  templateUrl: './process-creation-entity-selection.component.html'
})
export class ProcessCreationEntitySelectionComponent {

  // #region State accessors

  public get form(): ProcessCreationEntitySelectionFeatureForm {
    return this._processCreationEntitySelection.form;
  }

  // #endregion

  // #region Actions

  public submit(): void {

  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelection: ProcessCreationEntitySelectionFeature
  ) {}
}
