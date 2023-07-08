import { DynamicFeature } from "../feature.abstractions";
import { dispose, init } from "./error.actions";
import { ErrorState } from "./error.state";
import { id, message, state, status, statusCode, statusMessage } from "./error.selectors";
import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";

@Injectable()
export class ErrorFeature extends DynamicFeature<ErrorState, {
  id?: string;
  message?: string;
  statusCode?: number;
}>{

  constructor(store: Store) {
    super(store, state, init, dispose);
  }

  public readonly id$ = this.store.select(id);
  public readonly message$ = this.store.select(message);
  public readonly status$ = this.store.select(status);
  public readonly statusCode$ = this.store.select(statusCode);
  public readonly statusMessage$ = this.store.select(statusMessage);

}
