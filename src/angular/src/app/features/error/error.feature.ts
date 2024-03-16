import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { Feature } from "../feature.abstractions";
import { ErrorState } from "./rx/error.state";
import { dispose, init } from "./rx/error.actions";
import { correlationId, id, message, stackTrace, state, status } from "./rx/error.selectors";
import { Store, provideState } from "@ngrx/store";
import { provideEffects } from "@ngrx/effects";
import { ErrorEffects } from "./rx/error.effects";
import { FEATURE_NAME } from "./error.constants";
import { reducer } from "./rx/error.reducer";
import { ActivatedRouteSnapshot } from "@angular/router";

@Injectable()
export class ErrorFeature extends Feature<ErrorState> {

  readonly id$ = this.of(id);

  readonly correlationId$ = this.of(correlationId);

  readonly message$ = this.of(message);

  readonly stackTrace$ = this.of(stackTrace);

  readonly status$ = this.of(status);

  constructor(store: Store) {
    super(store, state);
  }

  protected override dispose$$$() {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot) {

    const statusCode = activatedRoute.queryParamMap.get('statusCode');

    return init({
      payload: {
        id: activatedRoute.queryParamMap.get('id') || undefined,
        correlationId: activatedRoute.queryParamMap.get('correlationId') || undefined,
        message: activatedRoute.queryParamMap.get('message') || undefined,
        stackTrace: activatedRoute.queryParamMap.get('stackTrace') || undefined,
        statusCode: (statusCode && parseInt(statusCode)) || undefined
      }
    });
  }
}

export const provideErrorFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    ErrorFeature
  ]),

  provideEffects(ErrorEffects),
  provideState(FEATURE_NAME, reducer)
];
