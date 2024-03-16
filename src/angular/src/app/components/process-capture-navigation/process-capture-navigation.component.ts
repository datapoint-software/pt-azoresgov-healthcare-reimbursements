import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ProcessCaptureNavigationIconComponent } from "../process-capture-navigation-icon/process-capture-navigation-icon.component";

@Component({
  imports: [
    CommonModule,
    ProcessCaptureNavigationIconComponent,
    RouterModule
  ],
  selector: 'app-process-capture-navigation',
  standalone: true,
  templateUrl: './process-capture-navigation.component.html'
})
export class ProcessCaptureNavigationComponent {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}
}
