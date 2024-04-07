import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";
import { ProcessCreationEntitySelectionFeature } from "../../features/process-creation-entity-selection/process-creation-entity-selection.feature";

@Component({
  imports: [ RouterLink ],
  selector: 'app-process-creation-navigation',
  standalone: true,
  templateUrl: './process-creation-navigation.component.html'
})
export class ProcessCreationNavigationComponent {

  // #region State accessors

  public get entitySelectionComplete(): boolean {
    return this._processCreationEntitySelection.complete;
  }

  public get entitySelectionEnabled(): boolean {
    return this._processCreationEntitySelection.enabled;
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelection: ProcessCreationEntitySelectionFeature
  ) {}
}
