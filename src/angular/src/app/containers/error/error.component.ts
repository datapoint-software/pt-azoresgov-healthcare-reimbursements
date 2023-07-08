import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ErrorFeature } from "../../features/error/error.feature";
import { takeUntil } from "rxjs";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-error',
  standalone: true,
  templateUrl: './error.component.html'
})
export class ErrorComponent {

  constructor(
    private readonly error: ErrorFeature
  ) {}

  public readonly id$ = this.error.id$
    .pipe(takeUntil(this.error.disposing$));

  public readonly message$ = this.error.message$
    .pipe(takeUntil(this.error.disposing$));

  public readonly status$ = this.error.status$
    .pipe(takeUntil(this.error.disposing$));
}
