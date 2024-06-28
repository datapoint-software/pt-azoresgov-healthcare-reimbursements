import { inject } from "@angular/core";
import { MainProcessCapturePatientComponent } from "@app/components/main-process-capture-patient/main-process-capture-patient.component";
import { MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCapturePatientGuard {

  public static canActivate(): boolean {

    const processCapture = inject(MainProcessCaptureFeature);

    processCapture.step = MainProcessCaptureFeatureStep.Patient;

    return true;
  }

  public static async canDeactivate(component: MainProcessCapturePatientComponent): Promise<boolean> {

    if (!component.form.controls.patient.pristine)
      await component.submitPatient();

    return true;
  }
}
