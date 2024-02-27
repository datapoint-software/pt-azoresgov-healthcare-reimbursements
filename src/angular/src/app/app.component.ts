import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router, RouterOutlet } from '@angular/router';
import { LoadingOverlayComponent } from './components/loading-overlay/loading-overlay.component';
import { Subject, takeUntil } from 'rxjs';
import { LoadingOverlayFeature } from './features/loading-overlay/loading-overlay.feature';

@Component({
  imports: [
    LoadingOverlayComponent,
    RouterOutlet
  ],
  selector: 'app-root',
  standalone: true,
  styleUrl: './app.component.scss',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, OnDestroy {

  private readonly destroy$ = new Subject<boolean>();

  constructor(
    private readonly loadingOverlay: LoadingOverlayFeature,
    private readonly router: Router
  ) {}

  ngOnDestroy(): void {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  ngOnInit(): void {

    const navigation = 'navigation';

    this.router.events
      .pipe(takeUntil(this.destroy$))
      .subscribe((e) => {

        if (e instanceof NavigationStart)
          this.loadingOverlay.enqueue(navigation);

        else if (e instanceof NavigationEnd || e instanceof NavigationError || e instanceof NavigationCancel)
          this.loadingOverlay.dequeue(navigation);
      });
  }


}
