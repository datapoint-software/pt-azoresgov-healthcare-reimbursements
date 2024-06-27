import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationCancel, NavigationEnd, NavigationError, NavigationStart, Router, RouterOutlet } from '@angular/router';
import { SuiLoadingOverlayComponent } from '@app/components/sui-loading-overlay/sui-loading-overlay.component';
import { SuiTaskOverlayComponent } from '@app/components/sui-task-overlay/sui-task-overlay.component';
import { CoreLoadingOverlayFeature } from '@app/features/core-loading-overlay/core-loading-overlay.feature';
import { Subscription, filter } from 'rxjs';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ SuiLoadingOverlayComponent, SuiTaskOverlayComponent, RouterOutlet],
  templateUrl: 'app.component.html',
  styleUrl: 'app.component.scss'
})
export class AppComponent implements OnInit, OnDestroy {

  private _subscriptions: Subscription[] = undefined!;

  constructor(
    private readonly _loadingOverlayFeature: CoreLoadingOverlayFeature,
    private readonly _router: Router
  ) {}

  ngOnDestroy(): void {

    for (const subscription of this._subscriptions)
      subscription.unsubscribe;

    this._subscriptions = undefined!;
  }

  ngOnInit(): void {

    const navigationLoadingOverlayId = `${AppComponent.name}.navigation`;

    this._loadingOverlayFeature.enqueue(navigationLoadingOverlayId);

    this._subscriptions = [

      this._router.events
        .pipe(filter(e => e instanceof NavigationStart))
        .subscribe(() => this._loadingOverlayFeature.enqueue(navigationLoadingOverlayId)),

      this._router.events
        .pipe(filter(e => e instanceof NavigationCancel || e instanceof NavigationEnd || e instanceof NavigationError))
        .subscribe(() => this._loadingOverlayFeature.dequeue(navigationLoadingOverlayId)),
    ];
  }

}
