import { makeEnvironmentProviders } from "@angular/core";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { ProcessCreationClient } from "./process-creation.client";
import { featureName } from "./process-creation.constants";
import { ProcessCreationEffects } from "./process-creation.effects";
import { ProcessCreationFeature } from "./process-creation.feature";
import { reducer } from "./process-creation.reducer";

export const provideProcessCreationFeature = () => [
  makeEnvironmentProviders([
    ProcessCreationClient,
    ProcessCreationFeature
  ]),

  provideEffects(ProcessCreationEffects),
  provideState(featureName, reducer)
];
