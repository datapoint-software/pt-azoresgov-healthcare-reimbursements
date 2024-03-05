import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { ProcessPatientCaptureFeature } from "../../features/process-patient-capture/process-patient-capture.feature";

export const canActivateProcessPatientCapture: CanActivateFn = async (ar, r) => {
  await inject(ProcessPatientCaptureFeature).init(ar, r);
  return true;
};

export const canDeactivateProcessPatientCapture: CanDeactivateFn<unknown> = async (c, ar, r) => {
  await inject(ProcessPatientCaptureFeature).dispose(ar, r);
  return true;
};
