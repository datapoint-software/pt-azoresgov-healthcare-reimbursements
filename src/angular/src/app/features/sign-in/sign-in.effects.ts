import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { catchErrorResponseWithStatusCode } from "../../app.operators";
import { dequeue, enqueue } from "../loading-overlay/loading-overlay.actions";
import { featureName } from "./sign-in.constants";
import { init, redirect, submit } from "./sign-in.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap, of, tap } from "rxjs";
import { SignInClient } from "./sign-in.client";
import { Store } from "@ngrx/store";
import { redirectUrl } from "./sign-in.selectors";
import { Router } from "@angular/router";

@Injectable()
export class SignInEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly router: Router,
    private readonly signInClient: SignInClient,
    private readonly store: Store
  ) {}

  public readonly initBegin$ = createEffect(() => this.actions$.pipe(
    ofType(init.begin),
    mergeMap((action) => [
      enqueue({ payload: { id: featureName } }),
      init.getOptions({ payload: { redirectUrl: action.payload.redirectUrl } })
    ])
  ));

  public readonly initGetOptions$ = createEffect(() => this.actions$.pipe(
    ofType(init.getOptions),
    mergeMap((action) => this.signInClient.getOptions().pipe(
      mergeMap((response) => [
        init.configure({
          payload: {
            ...response,
            redirectUrl: action.payload.redirectUrl
          }
        }),
        dequeue({ payload: { id: featureName } }),
      ])
    ))
  ));

  public readonly redirect$ = createEffect(() => this.actions$.pipe(
    ofType(redirect),
    concatLatestFrom(() => this.store.select(redirectUrl)),
    tap(([ _, redirectUrl ]) => this.router.navigateByUrl(
      redirectUrl || '/'
    ))
  ),
  { dispatch: false })

  public readonly submitBegin$ = createEffect(() => this.actions$.pipe(
    ofType(submit.begin),
    mergeMap((action) => [
      enqueue({ payload: { id: featureName } }),
      submit.post({ payload: { ...action.payload } })
    ])
  ));

  public readonly submitPost$ = createEffect(() => this.actions$.pipe(
    ofType(submit.post),
    map((action) => ({
      action,
      model: {
        emailAddress: action.payload.emailAddress,
        password: action.payload.password
      }
    })),
    mergeMap(({ model }) => this.signInClient.signIn(model)),
    mergeMap(() => [
      dequeue({ payload: { id: featureName } }),
      redirect()
    ]),
    catchErrorResponseWithStatusCode([ 400, 401, 403, 409 ], (response) => of(
      submit.error({
        payload: response.error
      })
    ))
  ));
}
