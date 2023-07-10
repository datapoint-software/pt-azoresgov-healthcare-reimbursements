import { Store } from "@ngrx/store";
import { StaticFeature } from "../feature.abstractions";
import { IdentityState } from "./identity.state";
import { state } from "./identity.selectors";
import { dispose, init } from "./identity.actions";
import { Injectable } from "@angular/core";

@Injectable()
export class IdentityFeature extends StaticFeature<IdentityState> {

  constructor(store: Store) {
    super(store, state, init, dispose);
  }
}
