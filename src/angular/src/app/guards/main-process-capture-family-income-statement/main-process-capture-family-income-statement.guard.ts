import { inject } from "@angular/core";
import { MainProcessCaptureFeatureStep } from "@app/features/main-process-capture/main-process-capture-feature.abstractions";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCaptureFamilyIncomeStatementGuard {

  public static canActivate(): boolean {

    const processCapture = inject(MainProcessCaptureFeature);

    processCapture.step = MainProcessCaptureFeatureStep.FamilyIncomeStatement;

    return true;
  }
}
