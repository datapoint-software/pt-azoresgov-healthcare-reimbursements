import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { basicMethod, basicMethodPersistentSessionsEnabled, error, method, state } from "./rx/sign-in.selectors";
import { dispose, init, signIn } from "./rx/sign-in.actions";
import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { FEATURE_NAME } from "./sign-in.constants";
import { reducer } from "./rx/sign-in.reducer";
import { SignInState } from "./rx/sign-in.state";
import { Store, provideState } from "@ngrx/store";
import { TypedAction } from "@ngrx/store/src/models";
import { SignInEffects } from "./rx/sign-in.effects";
import { provideEffects } from "@ngrx/effects";

@Injectable()
export class SignInFeature extends Feature<SignInState> {

  readonly basicMethod$ = this.createObservableFactory(basicMethod);

  readonly basicMethodPersistentSessionsEnabled$ = this.createObservableFactory(basicMethodPersistentSessionsEnabled);

  readonly error$ = this.createObservableFactory(error);

  readonly method$ = this.createObservableFactory(method);

  constructor(store: Store) {
    super(store, state);
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

  protected override dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {

    const redirectUrl = activatedRoute.queryParamMap.get("redirect") || undefined;

    return init({
      payload: {
        redirectUrl
      }
    });
  }

}
export const provideSignInFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    SignInFeature
  ]),

  provideEffects(SignInEffects),
  provideState(FEATURE_NAME, reducer)
];
