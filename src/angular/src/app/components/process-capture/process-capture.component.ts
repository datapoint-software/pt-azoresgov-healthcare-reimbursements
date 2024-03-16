import { CommonModule } from "@angular/common";
import { Component, OnDestroy } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ProcessCaptureNavigationComponent } from "../process-capture-navigation/process-capture-navigation.component";
import { RouterModule } from "@angular/router";

@Component({
  imports: [
    CommonModule,
    ProcessCaptureNavigationComponent,
    RouterModule
  ],
  selector: 'app-process-capture',
  standalone: true,
  styleUrl: './process-capture.component.scss',
  templateUrl: './process-capture.component.html'
})
export class ProcessCaptureComponent implements OnDestroy {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

  ngOnDestroy() {
    this.processCapture.dispose();
  }
}
