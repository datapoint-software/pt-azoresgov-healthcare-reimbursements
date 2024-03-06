import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ProcessPatientCaptureFeature } from "../../features/process-patient-capture/process-patient-capture.feature";
import { CommonModule } from "@angular/common";
import { take } from "rxjs";
import { ProcessStatusMenuComponent } from "../process-status-menu/process-status-menu.component";
import { IntegerPipe } from "../../pipes/integer.pipe";

@Component({
  imports: [
    CommonModule,
    IntegerPipe,
    ProcessStatusMenuComponent,
    ReactiveFormsModule
  ],
  selector: 'app-process-patient-capture',
  standalone: true,
  templateUrl: './process-patient-capture.component.html'
})
export class ProcessPatientCaptureComponent implements OnDestroy, OnInit {

  constructor(
    private readonly processPatientCapture: ProcessPatientCaptureFeature
  ) {}

  readonly patientHealthNumber$ = this.processPatientCapture.patientHealthNumber$;

  readonly patientName$ = this.processPatientCapture.patientName$;

  readonly patientTaxNumber$ = this.processPatientCapture.patientTaxNumber$;

  readonly process$ = this.processPatientCapture.process$;

  readonly processNumber$ = this.processPatientCapture.processNumber$;

  readonly patient = new FormGroup({
    addressLine1: new FormControl('', [ Validators.required, Validators.maxLength(128) ]),
    addressLine2: new FormControl('', [ Validators.maxLength(128) ]),
    addressLine3: new FormControl('', [ Validators.maxLength(128) ]),
    postalCode: new FormControl('', [ Validators.required, Validators.maxLength(16), Validators.pattern(/^\d{4}\-\d{3}$/) ]),
    postalCodeArea: new FormControl('', [ Validators.required, Validators.maxLength(128) ]),
    emailAddress: new FormControl('', [ Validators.email, Validators.maxLength(256) ]),
    faxNumber: new FormControl('', [ Validators.maxLength(16) ]),
    mobileNumber: new FormControl('', [ Validators.maxLength(16) ]),
    phoneNumber: new FormControl('', [ Validators.maxLength(16) ])
  });

  ngOnDestroy() {
    this.processPatientCapture.dispose();
  }

  ngOnInit() {

    this.processPatientCapture.patient$
      .pipe(take(1))
      .subscribe((patient) => {
        this.patient.setValue({
          addressLine1: patient.addressLine1,
          addressLine2: patient.addressLine2 || null,
          addressLine3: patient.addressLine3 || null,
          postalCode: patient.postalCode || null,
          postalCodeArea: patient.postalCodeArea || null,
          emailAddress: patient.emailAddress || null,
          faxNumber: patient.faxNumber || null,
          mobileNumber: patient.mobileNumber || null,
          phoneNumber: patient.phoneNumber || null
        })
      });
  }

  onSubmit() {

  }
}
