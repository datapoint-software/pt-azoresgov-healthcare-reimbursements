import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { CommonModule } from "@angular/common";
import { FormGroup } from "@angular/forms";

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

  readonly configuration = this.processCapture.configuration;

  readonly legalRepresentative = this.processCapture.legalRepresentative;

  readonly configurationRowVersionId$ = this.processCapture.configurationRowVersionId$;

  readonly patientRowVersionId$ = this.processCapture.patientRowVersionId$;

  readonly process$ = this.processCapture.process$;

  icon(formGroup: FormGroup) {

    if (formGroup.valid) {

      const enabledControl = formGroup.get('enabled');

      if (enabledControl && !enabledControl.value)
        return 'fe-eye-off text-muted';

      return 'fe-check text-success';
    }

    return 'fe-alert-triangle text-danger';
  }
}
