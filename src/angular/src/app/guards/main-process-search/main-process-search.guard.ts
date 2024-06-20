import { Provider, inject } from "@angular/core";
import { MainProcessSearchFeatureClient } from "@app/api/main-process-search-feature/main-process-search-feature.client";
import { MainProcessSearchFeature } from "@app/features/main-process-search/main-process-search.feature";

export class MainProcessSearchGuard {

  public static canActivate(): boolean {

    const processSearch = inject(MainProcessSearchFeature);

    processSearch.init();

    return true;
  }

  public static get providers(): Provider[] {
    return [
      MainProcessSearchFeature,
      MainProcessSearchFeatureClient
    ];
  }
}
