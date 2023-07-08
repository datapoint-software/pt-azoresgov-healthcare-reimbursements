import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { featureName } from "./loading-overlay.constants";
import { provideState } from "@ngrx/store";
import { reducer } from "./loading-overlay.reducer";
import { LoadingOverlayFeature } from "./loading-overlay.feature";

export const provideLoadingOverlayFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    LoadingOverlayFeature
  ]),

  provideState(featureName, reducer)
];
