import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ModalDirective } from "../../directives/modal.directive";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ProcessStatusPipe } from "../../pipes/process-status.pipe";
import { ProcessStatus } from "../../enums/process-status.enum";

@Component({
  imports: [
    CommonModule,
    ModalDirective,
    ProcessStatusPipe
  ],
  selector: 'app-process-capture-simulation',
  standalone: true,
  templateUrl: './process-capture-simulation.component.html'
})
export class ProcessCaptureSimulationComponent {

  public readonly ProcessStatus = ProcessStatus;

  public showConfirmationModal = false;

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

  continueClick() {
    this.showConfirmationModal = true;
  }
}
