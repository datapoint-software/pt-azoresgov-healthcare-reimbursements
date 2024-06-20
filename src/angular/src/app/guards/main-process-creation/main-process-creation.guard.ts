import { Provider, inject } from "@angular/core";
import { MainProcessCreationFeatureClient } from "@app/api/main-process-creation-feature/main-process-creation-feature.client";
import { MainProcessCreationFeature } from "@app/features/main-process-creation/main-process-creation.feature";

export class MainProcessCreationGuard {

  public static async canActivate(): Promise<boolean> {

    const processCreation = inject(MainProcessCreationFeature);

    await processCreation.init();

    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessCreationFeature,
      MainProcessCreationFeatureClient
    ];
  }
}
