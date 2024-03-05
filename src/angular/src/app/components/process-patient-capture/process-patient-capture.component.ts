import { Component, OnInit } from "@angular/core";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";
import { ProcessPatientCaptureFeature } from "../../features/process-patient-capture/process-patient-capture.feature";

@Component({
  imports: [
    ReactiveFormsModule
  ],
  selector: 'app-process-patient-capture',
  standalone: true,
  templateUrl: './process-patient-capture.component.html'
})
export class ProcessPatientCaptureComponent implements OnInit {

  constructor(
    private readonly processPatientCapture: ProcessPatientCaptureFeature
  ) {}

  readonly patient = new FormGroup({

  });

  onSubmit() {

  }

  ngOnInit(): void {

  }
}
