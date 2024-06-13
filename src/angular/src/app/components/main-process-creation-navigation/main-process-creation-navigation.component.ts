import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";
import { MainProcessCreationEntitySelectionFeature } from "@app/features/main-process-creation-entity-selection/main-process-creation-entity-selection.feature";

@Component({
  imports: [ RouterLink ],
  selector: 'app-main-process-creation-navigation',
  standalone: true,
  templateUrl: 'main-process-creation-navigation.component.html'
})
export class MainProcessCreationNavigationComponent {

  // #region State accessors

  public get entitySelectionComplete(): boolean {
    return this._processCreationEntitySelection.complete;
  }

  public get entitySelectionEnabled(): boolean {
    return this._processCreationEntitySelection.enabled;
  }

  // #endregion

  constructor(
    private readonly _processCreationEntitySelection: MainProcessCreationEntitySelectionFeature
  ) {}
}
