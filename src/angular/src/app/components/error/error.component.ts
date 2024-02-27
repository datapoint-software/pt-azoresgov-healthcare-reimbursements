import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ErrorFeature } from "../../features/error/error.feature";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-error',
  standalone: true,
  templateUrl: './error.component.html'
})
export class ErrorComponent {

  readonly correlationId$ = this.error.correlationId$();

  readonly id$ = this.error.id$();

  readonly message$ = this.error.message$();

  readonly stackTrace$ = this.error.stackTrace$();

  readonly status$ = this.error.status$();

  constructor(
    private readonly error: ErrorFeature
  ) {}


}
