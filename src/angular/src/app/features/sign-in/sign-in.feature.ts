import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { basicMethod, basicMethodPersistentSessionsEnabled, error, method, state } from "./rx/sign-in.selectors";
import { dispose, init, signIn } from "./rx/sign-in.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./sign-in.constants";
import { provideEffects } from "@ngrx/effects";
import { reducer } from "./rx/sign-in.reducer";
import { SignInEffects } from "./rx/sign-in.effects";
import { SignInState } from "./rx/sign-in.state";
import { Store, provideState } from "@ngrx/store";

@Injectable()
export class SignInFeature extends Feature<SignInState> {

  readonly basicMethod$ = this.of(basicMethod);

  readonly basicMethodPersistentSessionsEnabled$ = this.of(basicMethodPersistentSessionsEnabled);

  readonly error$ = this.of(error);

  readonly method$ = this.of(method);

  constructor(store: Store) {
    super(store, state, dispose, (r) => init({
      payload: {
        redirectUrl: r.queryParamMap.get('redirect') || undefined
      }
    }));
  }

  signIn(emailAddress: string, password: string, persistent: boolean) {
    this.dispatch(signIn({
      payload: {
        emailAddress,
        password,
        persistent
      }
    }));
  }
}

export const provideSignInFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    SignInFeature
  ]),

  provideEffects(SignInEffects),
  provideState(FEATURE_NAME, reducer)
];
