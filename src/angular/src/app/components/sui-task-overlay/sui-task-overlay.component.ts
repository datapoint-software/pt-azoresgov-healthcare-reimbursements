import { Component } from "@angular/core";
import { SuiLoadingSpinnerComponent } from "@app/components/sui-loading-spinner/sui-loading-spinner.component";
import { CoreTaskOverlayFeatureTask } from "@app/features/core-task-overlay/core-task-overlay-feature.abstractions";
import { CoreTaskOverlayFeature } from "@app/features/core-task-overlay/core-task-overlay.feature";

@Component({
  imports: [ SuiLoadingSpinnerComponent ],
  selector: 'app-sui-task-overlay',
  standalone: true,
  styleUrl: 'sui-task-overlay.component.scss',
  templateUrl: 'sui-task-overlay.component.html'
})
export class SuiTaskOverlayComponent {

  // #region State accessors

  public get tasks(): ReadonlyMap<string, Readonly<CoreTaskOverlayFeatureTask>> {
    return this._taskOverlay.tasks;
  }

  public get visible(): boolean {
    return this._taskOverlay.visible;
  }

  // #endregion

  constructor(
    private readonly _taskOverlay: CoreTaskOverlayFeature
  ) {}
}
