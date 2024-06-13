import { Component, OnDestroy, OnInit } from "@angular/core";
import { NavigationEnd, Router, RouterLink, RouterOutlet } from "@angular/router";
import { DataBsToggleCollapseDirective } from "@app/directives/data-bs-toggle-collapse/data-bs-toggle-collapse.directive";
import { Subscription, filter, map } from "rxjs";

@Component({
  imports: [ DataBsToggleCollapseDirective, RouterLink, RouterOutlet ],
  selector: 'app-main',
  standalone: true,
  templateUrl: 'main.component.html'
})
export class MainComponent implements OnDestroy, OnInit {

  // #region State

  private _path: string = undefined!;

  private _subscription: Subscription = undefined!;

  private _url: string = undefined!;

  // #endregion

  // #region State accessors

  public get path(): string {
    return this._path ?? '/';
  }

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

    this._navigationEnd(this._router.routerState.snapshot.url);

    this._subscription = this._router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .pipe(map(e => (e as NavigationEnd).url))
      .subscribe((url) => this._navigationEnd(url));
  }

  private _navigationEnd(url: string): void {

    const i = url.indexOf('?');

    this._path = i > -1 ? url.substring(0, i) : url;
    this._url = url;
  }
}
