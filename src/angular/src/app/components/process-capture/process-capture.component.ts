import { CommonModule } from "@angular/common";
import { Component, OnDestroy } from "@angular/core";
import { RouterModule } from "@angular/router";
import { ProcessCaptureNavigationComponent } from "../process-capture-navigation/process-capture-navigation.component";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { map } from "rxjs";

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
    private readonly processCapture: ProcessCaptureFeature
  ) {}

  readonly processNumber$ = this.processCapture.processNumber$;

  readonly writting$ = this.processCapture.writting$;

  ngOnDestroy() {
    this.processCapture.dispose();
  }
}
