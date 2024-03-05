import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

export const canActivateProcessCreation: CanActivateFn = async (ar, r) => {
  await inject(ProcessCreationFeature).init(ar, r);
  return true;
};

