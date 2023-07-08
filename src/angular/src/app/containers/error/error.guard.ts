import { inject } from "@angular/core";
import { CanActivateFn, CanDeactivateFn } from "@angular/router";
import { ErrorFeature } from "../../features/error/error.feature";
import { ErrorComponent } from "./error.component";

export const canActivateError: CanActivateFn = async (route) => {

  await inject(ErrorFeature).init({
    id: route.queryParams['id'] || undefined,
    message: route.queryParams['message'] || undefined,
    statusCode: (route.queryParams['status'] && parseInt(route.queryParams['status'])) || undefined
  });

  return true;
};

export const canDeactivateError: CanDeactivateFn<ErrorComponent> = async () => {
  await inject(ErrorFeature).dispose();
  return true;
}
