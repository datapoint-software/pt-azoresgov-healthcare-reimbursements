import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { IdentityClient } from "./identity.client";
import { IdentityFeature } from "./identity.feature";
import { provideEffects } from "@ngrx/effects";
import { IdentityEffects } from "./identity.effects";
import { provideState } from "@ngrx/store";
import { featureName } from "./identity.constants";
import { reducer } from "./identity.reducer";

export const provideIdentityFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    IdentityClient,
    IdentityFeature
  ]),

  provideEffects(IdentityEffects),
  provideState(featureName, reducer)
];
