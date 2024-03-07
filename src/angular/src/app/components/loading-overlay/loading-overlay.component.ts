import { CommonModule } from "@angular/common";
import { Component, Injectable } from "@angular/core";
import { LoadingOverlayFeature } from "../../features/loading-overlay/loading-overlay.feature";
import { LoadingSpinnerComponent } from "../loading-spinner/loading-spinner.component";
import { map, tap } from "rxjs";

@Component({
  imports: [
    CommonModule,
    LoadingSpinnerComponent
  ],
  selector: 'app-loading-overlay',
  standalone: true,
  styleUrl: './loading-overlay.component.scss',
  templateUrl: './loading-overlay.component.html'
})
export class LoadingOverlayComponent {

  constructor(
    private readonly loadingOverlay: LoadingOverlayFeature
  ) {}

  readonly visible$ = this.loadingOverlay.items$.pipe(
    map((items) => Object.keys(items).length > 0)
  );
}
