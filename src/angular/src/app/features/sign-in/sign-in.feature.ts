import { dispose, init, signIn } from "./sign-in.actions";
import { DynamicFeature } from "../feature.abstractions";
import { Injectable } from "@angular/core";
import { SignInState } from "./sign-in.state";
import { authenticationEnabled, authenticationError, authenticationPersistentEnabled, state } from "./sign-in.selectors";
import { Store } from "@ngrx/store";

@Injectable()
export class SignInFeature extends DynamicFeature<SignInState, {
  redirectUrl?: string;
}> {

  constructor(store: Store) {
    super(store, state, init, dispose);
  }

  public readonly authenticationEnabled$ = this.createObservableOf(authenticationEnabled);
  public readonly authenticationError$ = this.createObservableOf(authenticationError);
  public readonly authenticationPersistentEnabled$ = this.createObservableOf(authenticationPersistentEnabled);

  public signIn(payload: {
    emailAddress: string;
    password: string;
    persistent: boolean;
  }) {
    this.store.dispatch(signIn({ payload }));
  }
}
