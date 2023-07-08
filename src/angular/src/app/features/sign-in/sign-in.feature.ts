import { authentication, authenticationEnabled, authenticationPersistentEnabled, error, state } from "./sign-in.selectors";
import { DynamicFeature } from "../feature.abstractions";
import { dispose, init, submit } from "./sign-in.actions";
import { Injectable } from "@angular/core";
import { SignInState } from "./sign-in.state";
import { Store } from "@ngrx/store";

@Injectable()
export class SignInFeature extends DynamicFeature<SignInState, {
  redirectUrl?: string
}> {

  constructor(store: Store) {
    super(store, state, init.begin, dispose);
  }

  public readonly authentication$ = this.store.select(authentication);
  public readonly authenticationEnabled$ = this.store.select(authenticationEnabled);
  public readonly authenticationPersistentEnabled$ = this.store.select(authenticationPersistentEnabled);
  public readonly error$ = this.store.select(error);

  submit(payload: {
    emailAddress: string,
    password: string,
    persistent: boolean
  }) {
    this.store.dispatch(
      submit.begin({ payload })
    );
  }
}
