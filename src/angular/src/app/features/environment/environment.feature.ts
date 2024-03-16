import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { provideEffects } from "@ngrx/effects";
import { Store, provideState } from "@ngrx/store";
import { Feature } from "../feature.abstractions";
import { dispose, init } from "./rx/environment.actions";
import { EnvironmentEffects } from "./rx/environment.effects";
import { reducer } from "./rx/environment.reducer";
import { nature, productVersion, state } from "./rx/environment.selectors";
import { EnvironmentState } from "./rx/environment.state";

const FEATURE_NAME = 'environment';

@Injectable()
export class EnvironmentFeature extends Feature<EnvironmentState> {

  public readonly nature$ = this.of(nature);

  public readonly productVersion$ = this.of(productVersion);

  constructor(store: Store) {
    super(store, state);
  }

  protected override dispose$$$() {
    return dispose();
  }

  protected override init$$$() {
    return init();
  }
}

export const provideEnvironmentFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    EnvironmentFeature
  ]),

  provideEffects(EnvironmentEffects),
  provideState(FEATURE_NAME, reducer)
];

