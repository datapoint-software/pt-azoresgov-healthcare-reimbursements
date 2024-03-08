import { Component } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-special-terms',
  standalone: true,
  templateUrl: './process-capture-special-terms.component.html'
})
export class ProcessCaptureSpecialTermsComponent {

  constructor(
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly configuration = this.processCapture.configuration;
}
