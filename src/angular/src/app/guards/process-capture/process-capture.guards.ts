import { CanActivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { ProcessCaptureFeature } from "../../features/process-capture/process-capture.feature";

export const canActivateProcessCapture: CanActivateFn = async (ar, r) => {
  await inject(ProcessCaptureFeature).init(ar, r);
  return true;
};

