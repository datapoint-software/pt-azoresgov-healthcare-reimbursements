import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { RouterLink, RouterOutlet } from "@angular/router";
import { BootstrapCollapseComponent } from "../../components/bootstrap/collapse/boostrap-collapse.component";
import { IdentityFeature } from "../../features/identity/identity.feature";

@Component({
  imports: [
    BootstrapCollapseComponent,
    CommonModule,
    RouterOutlet,
    RouterLink
  ],
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html'
})
export class LayoutComponent {

  constructor(
    private readonly identity: IdentityFeature
  ) {}

  public readonly authorize$ = this.identity.authorize$;

  public readonly processCreationEnabled$ = this.authorize$([
    'process-search',
    'process-creation'
  ]);

  public readonly processEnabled$ = this.processCreationEnabled$;
}
