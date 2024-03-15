import { CommonModule } from "@angular/common";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from "@angular/forms";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { Subject, take, takeUntil } from "rxjs";
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
export class ProcessCapturePatientComponent {

  private readonly destroy$ = new Subject<boolean>();

  constructor(
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly patientHealthNumber$ = this.processCapture.patientHealthNumber$;

  readonly patientName$ = this.processCapture.patientName$;

  readonly patientTaxNumber$ = this.processCapture.patientTaxNumber$;

  readonly patient = this.processCapture.patient;

  onSubmit() {

  }

}
