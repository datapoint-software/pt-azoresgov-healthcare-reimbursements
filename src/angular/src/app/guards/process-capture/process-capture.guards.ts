import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";
import { ProcessCaptureComponent } from "../../components/process-capture/process-capture.component";
import { map } from "rxjs";

export const canActivateProcessCapture: CanActivateFn = async (ar, r) => {
  await inject(ProcessCaptureFeature).init(ar, r);
  return true;
};

export const canDeactivateProcessCapture: CanDeactivateFn<ProcessCaptureComponent> = () => {
  return inject(ProcessCaptureFeature).writting$.pipe(map(w => !w));
};
