import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ProcessCaptureSimulationManager } from "../../features/process-capture/managers/process-capture-simulation.manager";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-process-capture-simulation',
  standalone: true,
  templateUrl: './process-capture-simulation.component.html'
})
export class ProcessCaptureSimulationComponent {

  constructor(
    public readonly simulation: ProcessCaptureSimulationManager
  ) {}
}
