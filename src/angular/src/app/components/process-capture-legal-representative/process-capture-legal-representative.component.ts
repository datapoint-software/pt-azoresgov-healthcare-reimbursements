import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [
    CommonModule,
    FormGroupComponent,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-legal-representative',
  standalone: true,
  templateUrl: './process-capture-legal-representative.component.html'
})
export class ProcessCaptureLegalRepresentativeComponent {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}
}
