import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { EnvironmentClient } from "./environment.client";
import { featureName } from "./environment.constants";
import { EnvironmentEffects } from "./environment.effects";
import { EnvironmentFeature } from "./environment.feature";
import { reducer } from "./environment.reducer";

export const provideEnvironmentFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    EnvironmentClient,
    EnvironmentFeature
  ]),

  provideEffects(EnvironmentEffects),
  provideState(featureName, reducer)
];
