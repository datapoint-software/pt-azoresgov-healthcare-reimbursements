import { dispose, init } from "./identity.actions";
import { IdentityState } from "./identity.state";
import { Injectable } from "@angular/core";
import { state } from "./identity.selectors";
import { StaticFeature } from "../feature.abstractions";
import { Store } from "@ngrx/store";

@Injectable()
export class IdentityFeature extends StaticFeature<IdentityState> {

  constructor(store: Store) {
    super(store, state, init, dispose);
  }
}
