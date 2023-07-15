import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { featureName } from "./identity.constants";
import { IdentityClient } from "./identity.client";
import { IdentityEffects } from "./identity.effects";
import { IdentityFeature } from "./identity.feature";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { reducer } from "./identity.reducer";

export const provideIdentityFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    IdentityClient,
    IdentityFeature
  ]),

  provideEffects(IdentityEffects),
  provideState(featureName, reducer)
];
