import { Component, Injectable } from "@angular/core";
import { LoadingOverlayFeature } from "../../features/loading-overlay/loading-overlay.feature";
import { map, tap } from "rxjs";
import { CommonModule } from "@angular/common";

@Component({
  imports: [
    CommonModule
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

  readonly visible$ = this.loadingOverlay.items$().pipe(
    map((items) => Object.keys(items).length > 0)
  );
}
