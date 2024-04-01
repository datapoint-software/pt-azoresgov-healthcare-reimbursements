import { Component } from "@angular/core";
import { LoadingOverlayFeature } from "../../features/loading-overlay/loading-overlay.feature";
import { LoadingSpinnerComponent } from "../loading-spinner/loading-spinner.component";

@Component({
  imports: [
    LoadingSpinnerComponent
  ],
  selector: 'app-loading-overlay',
  standalone: true,
  styleUrl: './loading-overlay.component.scss',
  templateUrl: './loading-overlay.component.html'
})
export class LoadingOverlayComponent {

  // #region State accessors

  protected get visible() {
    return this._loadingOverlay.visible;
  }

  // #endregion

  constructor(
    private readonly _loadingOverlay: LoadingOverlayFeature
  ) {}
}
