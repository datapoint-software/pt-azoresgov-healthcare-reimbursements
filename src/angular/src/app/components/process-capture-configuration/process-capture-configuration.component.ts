import { AfterViewInit, Component } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-configuration',
  standalone: true,
  templateUrl: './process-capture-configuration.component.html'
})
export class ProcessCaptureConfigurationComponent implements AfterViewInit {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

  public ngAfterViewInit() {
    this.processCapture.configuration.seen();
  }
}
