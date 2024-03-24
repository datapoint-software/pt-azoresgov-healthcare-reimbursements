import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { PaymentMethod } from "../../enums/payment-method.enum";
import { PaymentReceiver } from "../../enums/payment-receiver.enum";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [
    CommonModule,
    FormGroupComponent,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-payment',
  standalone: true,
  templateUrl: './process-capture-payment.component.html'
})
export class ProcessCapturePaymentComponent implements OnInit {

  public readonly PaymentMethod = PaymentMethod;

  public readonly PaymentReceiver = PaymentReceiver;

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}

  public ngOnInit() {
    this.processCapture.payment.watch();
  }
}
