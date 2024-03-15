import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormGroupComponent } from "../form-group/form-group.component";
import { invalid } from "../../helpers/reactive-forms.helper";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ReactiveFormsModule } from "@angular/forms";

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
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly legalRepresentative = this.processCapture.legalRepresentative;

  readonly processNumber$ = this.processCapture.processNumber$;

  readonly invalid = invalid;

  onSubmit() {

  }
}
