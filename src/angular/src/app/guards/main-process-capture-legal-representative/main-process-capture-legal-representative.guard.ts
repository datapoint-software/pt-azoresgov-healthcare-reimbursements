import { inject } from "@angular/core";
import { MainProcessCaptureLegalRepresentativeComponent } from "@app/components/main-process-capture-legal-representative/main-process-capture-legal-representative.component";
import { MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCaptureLegalRepresentativeGuard {

  public static canActivate(): boolean {

    const processCapture = inject(MainProcessCaptureFeature);

    processCapture.step = MainProcessCaptureFeatureStep.LegalRepresentative;

    return true;
  }

  public static async canDeactivate(component: MainProcessCaptureLegalRepresentativeComponent): Promise<boolean> {

    if (component.form.controls.legalRepresentative.enabled)
      if (!component.form.controls.legalRepresentative.pristine)
        await component.submitLegalRepresentative();

    return true;
  }
}
