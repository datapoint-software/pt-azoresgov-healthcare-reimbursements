import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ModalDirective } from "../../directives/modal.directive";
import { ProcessStatus } from "../../enums/process-status.enum";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ProcessStatusPipe } from "../../pipes/process-status.pipe";
import { RouterModule } from "@angular/router";

@Component({
  imports: [
    CommonModule,
    ModalDirective,
    ProcessStatusPipe,
    RouterModule
  ],
  selector: 'app-process-capture-simulation',
  standalone: true,
  templateUrl: './process-capture-simulation.component.html'
})
export class ProcessCaptureSimulationComponent {

  public readonly ProcessStatus = ProcessStatus;

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}
}
