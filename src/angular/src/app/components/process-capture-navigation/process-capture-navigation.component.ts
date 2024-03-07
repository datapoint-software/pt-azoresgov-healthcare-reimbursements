import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { CommonModule } from "@angular/common";
import { map } from "rxjs";

const icon = (valid: boolean) => valid ? 'fe-check text-success' :
  'fe-alert-triangle text-danger';

@Component({
  imports: [
    CommonModule,
    RouterModule
  ],
  selector: 'app-process-capture-navigation',
  standalone: true,
  templateUrl: './process-capture-navigation.component.html'
})
export class ProcessCaptureNavigationComponent {

  constructor(
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly patient = this.processCapture.patient;

  readonly specialTerms = this.processCapture.specialTerms;

  readonly process$ = this.processCapture.process$;

  icon(valid: boolean) {
    return valid ? 'fe-check text-success' : 'fe-alert-triangle text-danger';
  }
}
