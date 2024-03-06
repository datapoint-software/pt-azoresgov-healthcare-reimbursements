import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { Store, provideState } from "@ngrx/store";
import { nature, state, productVersion } from "./rx/environment.selectors";
import { dispose, init } from "./rx/environment.actions";
import { EnvironmentState } from "./rx/environment.state";
import { reducer } from "./rx/environment.reducer";
import { EnvironmentEffects } from "./rx/environment.effects";
import { provideEffects } from "@ngrx/effects";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { TypedAction } from "@ngrx/store/src/models";

const FEATURE_NAME = 'environment';

@Injectable()
export class EnvironmentFeature extends Feature<EnvironmentState> {

  public readonly nature$ = this.of(nature);

  public readonly productVersion$ = this.of(productVersion);

  constructor(store: Store) {
    super(store, state, dispose, init);
  }
}

export const provideEnvironmentFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    EnvironmentFeature
  ]),

  provideEffects(EnvironmentEffects),
  provideState(FEATURE_NAME, reducer)
];

