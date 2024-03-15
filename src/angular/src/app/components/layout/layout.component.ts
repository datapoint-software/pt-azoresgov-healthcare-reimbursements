import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { filter, map, mergeMap, of, startWith, tap } from "rxjs";
import { NavigationEnd, Router, RouterModule } from "@angular/router";
import { CollapseDirective } from "../../directives/collapse.directive";

@Component({
  imports: [
    CollapseDirective,
    CommonModule,
    RouterModule
  ],
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html'
})
export class LayoutComponent {

  constructor(
    private readonly router: Router
  ) {}

  readonly url$ = this.router.events.pipe(
    filter(e => e instanceof NavigationEnd),
    map(e => e as NavigationEnd),
    map(e => e.url),
    startWith(this.router.url)
  );

  readonly processUrl$ = this.url$.pipe(map(u => u.startsWith('/processes')));

  readonly processCreationUrl$ = this.url$.pipe(map(u => u === '/processes/_'));

  readonly processSearchUrl$ = this.url$.pipe(map(u => u === '/processes'));
}
