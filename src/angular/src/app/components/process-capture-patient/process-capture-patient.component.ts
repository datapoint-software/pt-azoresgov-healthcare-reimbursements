import { CommonModule } from "@angular/common";
import { AfterViewInit, Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { IntegerPipe } from "../../pipes/integer.pipe";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [
    CommonModule,
    FormGroupComponent,
    IntegerPipe,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-patient',
  standalone: true,
  templateUrl: './process-capture-patient.component.html'
})
export class ProcessCapturePatientComponent implements AfterViewInit {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

  public ngAfterViewInit() {
    this.processCapture.patient.seen();
  }
}
