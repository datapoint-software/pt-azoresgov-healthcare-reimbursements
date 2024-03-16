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
  selector: 'app-process-capture-unemployment',
  standalone: true,
  templateUrl: './process-capture-unemployment.component.html'
})
export class ProcessCaptureUnemploymentComponent {

  constructor(
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly unemployment = this.processCapture.unemployment;

}
