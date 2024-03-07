import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { inject } from "@angular/core";
import { ProcessSearchFeature } from "../../features/process-search/process-search.feature";

export const canActivateProcessSearch: CanActivateFn = async (ar, r) => {
  await inject(ProcessSearchFeature).init(ar, r);
  return true;
};
