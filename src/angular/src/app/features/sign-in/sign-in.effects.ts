import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { authenticate } from "../identity/identity.actions";
import { catchErrorResponseWithConflictStatusCode } from "../../app.operators";
import { dequeue, enqueue } from "../loading-overlay/loading-overlay.actions";
import { featureName } from "./sign-in.constants";
import { init, initConfigure, initGetOptions, redirect, signIn, signInError, signInSignIn } from "./sign-in.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap, of, tap } from "rxjs";
import { redirectUrl } from "./sign-in.selectors";
import { Router } from "@angular/router";
import { SignInClient } from "./sign-in.client";
import { Store } from "@ngrx/store";

@Injectable()
export class SignInEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly router: Router,
    private readonly signInClient: SignInClient,
    private readonly store: Store
  ) {}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    map(({ payload }) => initGetOptions({ payload }))
  ));

  public readonly initGetOptions$ = createEffect(() => this.actions$.pipe(
    ofType(initGetOptions),
    mergeMap(({ payload }) => this.signInClient.getOptions().pipe(
      map((response) => initConfigure({
        payload: {
          ...response,
          ...payload
        }
      }))
    ))
  ));

  public readonly redirect$ = createEffect(() => this.actions$.pipe(
    ofType(redirect),
    concatLatestFrom(() => this.store.select(redirectUrl)),
    tap(([ _, redirectUrl ]) => {
      this.router.navigateByUrl(redirectUrl || '/');
    })
  ),
  { dispatch: false });

  public readonly signIn$ = createEffect(() => this.actions$.pipe(
    ofType(signIn),
    mergeMap(({ payload }) => [
      enqueue({
        payload: {
          id: featureName
        }
      }),
      signInSignIn({ payload })
    ])
  ));

  public readonly signInSignIn$ = createEffect(() => this.actions$.pipe(
    ofType(signInSignIn),
    mergeMap(({ payload }) => this.signInClient.signIn({ ...payload }).pipe(
      mergeMap((response) => [
        authenticate({
          payload: {
            ...response,
            persistent: payload.persistent
          }
        }),
        redirect(),
        dequeue({
          payload: {
            id: featureName
          }
        })
      ])
    )),
    catchErrorResponseWithConflictStatusCode((response) => of(
      signInError({
        payload: response.error
      })
    ))
  ));
}
