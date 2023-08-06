import { inject } from "@angular/core";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

export const canActivateProcessCreation = async () => {
  await inject(ProcessCreationFeature).init();
  return true;
};
