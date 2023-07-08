import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { LoadingOverlayComponent } from './components/loading-overlay/loading-overlay.component';
import { RouterOutlet } from '@angular/router';
import { LoadingOverlayFeature } from './features/loading-overlay/loading-overlay.feature';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule,
    LoadingOverlayComponent,
    RouterOutlet
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  constructor(
    private readonly loadingOverlayFeature: LoadingOverlayFeature
  ) {}

  public readonly loadingOverlayVisible$ = this.loadingOverlayFeature.visible$;

}
