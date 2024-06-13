import { Component } from "@angular/core";
import { SuiLoadingSpinnerComponent } from "@app/components/sui-loading-spinner/sui-loading-spinner.component";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";

@Component({
  imports: [
    SuiLoadingSpinnerComponent
  ],
  selector: 'app-sui-loading-overlay',
  standalone: true,
  styleUrl: 'sui-loading-overlay.component.scss',
  templateUrl: 'sui-loading-overlay.component.html'
})
export class SuiLoadingOverlayComponent {

  // #region State accessors

  protected get visible() {
    return this._loadingOverlay.visible;
  }

  // #endregion

  constructor(
    private readonly _loadingOverlay: CoreLoadingOverlayFeature
  ) {}
}
