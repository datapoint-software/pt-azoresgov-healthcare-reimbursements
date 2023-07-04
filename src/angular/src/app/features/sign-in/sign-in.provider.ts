import { EnvironmentProviders, makeEnvironmentProviders } from "@angular/core";
import { featureName } from "./sign-in.constants";
import { provideEffects } from "@ngrx/effects";
import { provideState } from "@ngrx/store";
import { reducer } from "./sign-in.reducer";
import { SignInClient } from "./sign-in.client";
import { SignInEffects } from "./sign-in.effects";
import { SignInFeature } from "./sign-in.feature";

export const provideSignInFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    SignInClient,
    SignInFeature
  ]),

  provideEffects(SignInEffects),
  provideState(featureName, reducer)
];
