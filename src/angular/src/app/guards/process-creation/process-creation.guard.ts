import { inject } from "@angular/core";
import { IdentityFeature } from "../../features/identity/identity.feature";

export class ProcessCreationGuard {

  public static async canActivate(): Promise<boolean> {

    const identity = inject(IdentityFeature);

    return identity.administrative;
  }
}
