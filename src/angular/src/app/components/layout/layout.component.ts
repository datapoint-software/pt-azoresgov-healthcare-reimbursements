import { Component, OnDestroy, OnInit } from "@angular/core";
import { NavigationEnd, Router, RouterLink, RouterOutlet } from "@angular/router";
import { Subscription, filter, map } from "rxjs";
import { BootstrapToggleCollapseDirective } from "../../directives/bootstrap/data-bs-toggle-collapse.directive";

@Component({
  imports: [ BootstrapToggleCollapseDirective, RouterLink, RouterOutlet ],
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html'
})
export class LayoutComponent implements OnDestroy, OnInit {

  // #region State

  private _subscription: Subscription = undefined!;

  private _url: string = undefined!;

  // #endregion

  // #region State accessors

  public get url(): string {
    return this._url ?? '/';
  }

  // #endregion

  constructor(
    private readonly _router: Router
  ) {}

  public ngOnDestroy(): void {
    this._subscription.unsubscribe();
    this._subscription = undefined!;
  }

  public ngOnInit(): void {
    this._subscription = this._router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .pipe(map(e => (e as NavigationEnd).url))
      .subscribe((url) => this._url = url);
  }
}
