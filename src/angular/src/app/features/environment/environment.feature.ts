import { Injectable } from "@angular/core";
import { StaticFeature } from "../feature.abstractions";
import { EnvironmentState } from "./environment.state";
import { Store } from "@ngrx/store";
import { debugSymbols, fileVersion, productVersion, production, state } from "./environment.selectors";
import { dispose, init } from "./environment.actions";
import { map } from "rxjs";

@Injectable()
export class EnvironmentFeature extends StaticFeature<EnvironmentState> {

  constructor(store: Store) {
    super(store, state, init, dispose);
  }

  public readonly debugSymbols$ = this.createObservableOf(debugSymbols);
  public readonly fileVersion$ = this.createObservableOf(fileVersion);
  public readonly productVersion$ = this.createObservableOf(productVersion);
  public readonly production$ = this.createObservableOf(production);

  public readonly development$ = this.production$.pipe(
    map(production => !production)
  );
}
