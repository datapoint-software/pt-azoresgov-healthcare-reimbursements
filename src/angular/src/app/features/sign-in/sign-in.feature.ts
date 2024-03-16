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
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { map } from "rxjs";
import { AuthenticationMethod } from "../../enums/authentication-method.enum";
import { ActivatedRoute, ActivatedRouteSnapshot } from "@angular/router";

@Injectable()
export class SignInFeature extends Feature<SignInState> {

  readonly form = new FormGroup({
    emailAddress: new FormControl('', [ Validators.required, Validators.maxLength(256), Validators.email ]),
    password: new FormControl('', [ Validators.required, Validators.maxLength(1024) ]),
    persistent: new FormControl(false)
  });

  readonly basicMethod$ = this.of(basicMethod);

  readonly basicMethodPersistentSessionsEnabled$ = this.of(basicMethodPersistentSessionsEnabled);

  readonly basicMethodVisible$ = this.of(method).pipe(
    map((method) => method === AuthenticationMethod.Basic));

  readonly error$ = this.of(error);

  readonly method$ = this.of(method);

  constructor(store: Store) {
    super(store, state);
  }

  protected override dispose$$$() {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot) {
    return init({
      payload: {
        redirectUrl: activatedRoute.queryParamMap.get('redirect') || undefined
      }
    })
  }

  signIn() {

    if (!this.form.valid)
      return;

    this.dispatch(signIn({
      payload: {
        emailAddress: this.form.value.emailAddress!,
        password: this.form.value.password!,
        persistent: this.form.value.persistent!
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
