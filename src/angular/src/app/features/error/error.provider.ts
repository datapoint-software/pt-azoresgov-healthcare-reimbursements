import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { ErrorEffects } from "./error.effects";
import { ErrorFeature } from "./error.feature";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { reducer } from "./error.reducer";
import { featureName } from "./error.constants";

export const provideErrorFeature = (): Array<EnvironmentProviders> => [
  makeEnvironmentProviders([
    ErrorFeature
  ]),
  provideEffects(ErrorEffects),
  provideState(featureName, reducer)
];
