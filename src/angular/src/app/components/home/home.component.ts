import { Component } from "@angular/core";
import { EnvironmentFeature } from "../../features/environment/environment.feature";
import { CommonModule } from "@angular/common";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html'
})
export class HomeComponent {

  constructor(
    private readonly environment: EnvironmentFeature
  ) {}

  readonly productVersion$ = this.environment.productVersion$;
}
