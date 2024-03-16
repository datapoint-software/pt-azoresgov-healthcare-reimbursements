import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormGroupComponent } from "../form-group/form-group.component";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { PaymentMethod } from "../../enums/payment-method.enum";
import { PaymentReceiver } from "../../enums/payment-receiver.enum";

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
export class ProcessCapturePaymentComponent {

  public readonly PaymentMethod = PaymentMethod;

  public readonly PaymentReceiver = PaymentReceiver;

  constructor(
    public readonly processCapture: ProcessCaptureFeature
  ) {}


}
