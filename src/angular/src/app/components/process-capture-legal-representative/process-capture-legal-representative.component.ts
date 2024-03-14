import { Component, Injectable } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-legal-representative',
  standalone: true,
  templateUrl: './process-capture-legal-representative.component.html'
})
export class ProcessCaptureLegalRepresentativeComponent {

  constructor(
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly processNumber$ = this.processCapture.processNumber$;
}
