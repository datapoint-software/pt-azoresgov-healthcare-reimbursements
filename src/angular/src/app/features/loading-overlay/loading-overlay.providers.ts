import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { featureName } from "./loading-overlay.constants";
import { LoadingOverlayEffects } from "./loading-overlay.effects";
import { LoadingOverlayService } from "./loading-overlay.service";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { reducer } from "./loading-overlay.reducer";

export const provideLoadingOverlayFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    LoadingOverlayService
  ]),

  provideEffects(LoadingOverlayEffects),
  provideState(featureName, reducer)
];
