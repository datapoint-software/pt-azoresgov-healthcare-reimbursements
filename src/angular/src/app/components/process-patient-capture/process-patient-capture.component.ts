import { Component, OnDestroy } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { ProcessPatientCaptureFeature } from "../../features/process-patient-capture/process-patient-capture.feature";
import { CommonModule } from "@angular/common";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-process-patient-capture',
  standalone: true,
  templateUrl: './process-patient-capture.component.html'
})
export class ProcessPatientCaptureComponent implements OnDestroy {

  constructor(
    private readonly processPatientCapture: ProcessPatientCaptureFeature
  ) {}

  readonly processNumber$ = this.processPatientCapture.processNumber$;

  readonly patient = new FormGroup({

  });

  ngOnDestroy() {
    this.processPatientCapture.dispose();
  }

  onSubmit() {

  }
}
