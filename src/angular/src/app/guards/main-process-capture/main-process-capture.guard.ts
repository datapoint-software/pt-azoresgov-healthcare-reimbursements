import { Provider } from "@angular/core";
import { MainProcessCaptureFeature } from "@app/features/main-process-capture/main-process-capture.feature";

export class MainProcessCaptureGuard {

  public static canActivate(): boolean {
    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessCaptureFeature
    ];
  }
}
