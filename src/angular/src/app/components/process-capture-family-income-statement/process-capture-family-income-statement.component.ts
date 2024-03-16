import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ReactiveFormsModule } from "@angular/forms";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [
    CommonModule,
    FormGroupComponent,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-family-income-statement',
  standalone: true,
  templateUrl: './process-capture-family-income-statement.component.html'
})
export class ProcessCaptureFamilyIncomeStatementComponent {

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

}
