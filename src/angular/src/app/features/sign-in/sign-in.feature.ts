import { DisposableFeature } from "../feature.abstractions";
import { Injectable } from "@angular/core";
import { SignInPayload } from "./sign-in.payloads";
import { SignInState } from "./sign-in.state";
import { Store } from "@ngrx/store";

import * as actions from './sign-in.actions';
import * as selectors from './sign-in.selectors';

@Injectable()
export class SignInFeature extends DisposableFeature<SignInState> {

  constructor(protected readonly store: Store) {
    super();
  }

  public readonly configure$$ = actions.configure;
  public readonly dispose$$ = actions.dispose;
  public readonly init$$ = actions.init;
  public readonly error$$ = actions.error;
  public readonly redirect$$ = actions.redirect;
  public readonly signIn$$ = actions.signIn;

  public readonly authentication$ = this.store.select(selectors.authentication);
  public readonly authenticationEnabled$ = this.store.select(selectors.authenticationEnabled);
  public readonly authenticationPersistentEnabled$ = this.store.select(selectors.authenticationPersistentEnabled);
  public readonly error$ = this.store.select(selectors.error);
  public readonly state$ = this.store.select(selectors.state);

  signIn(payload: SignInPayload) {
    this.store.dispatch(this.signIn$$({ payload }));
  }
}
