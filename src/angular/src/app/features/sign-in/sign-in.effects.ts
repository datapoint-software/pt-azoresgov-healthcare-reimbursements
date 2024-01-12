import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { dequeue, enqueue } from "../loading-overlay/loading-overlay.actions";
import { init, initConfigure, initGetOptions, signIn, signInPost, signInPostError, signInRedirect } from "./sign-in.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap, of, tap } from "rxjs";
import { SignInClient } from "./sign-in.client";
import { authenticate } from "../identity/identity.actions";
import { catchErrorResponseWithConflictStatusCode } from "../../app.operators";
import { Store } from "@ngrx/store";
import { Router } from "@angular/router";
import { redirectUrl } from "./sign-in.selectors";

const loadingScreen = (id: string) => ({
  payload: {
    id
  }
});

@Injectable()
export class SignInEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly router: Router,
    private readonly signInClient: SignInClient,
    private readonly store: Store
  ){}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(({ payload }) => of(
      enqueue(loadingScreen(init.type)),
      initGetOptions({ payload })
    ))
  ));

  public readonly initGetOptions$ = createEffect(() => this.actions$.pipe(
    ofType(initGetOptions),
    mergeMap((action) => this.signInClient.getOptions().pipe(
      map((response) => initConfigure({
        payload: {
          ...response,
          ...action.payload
        }
      }))
    ))
  ));

  public readonly initConfigure$ = createEffect(() => this.actions$.pipe(
    ofType(initConfigure),
    map(() => dequeue(loadingScreen(init.type)))
  ));

  public readonly signIn$ = createEffect(() => this.actions$.pipe(
    ofType(signIn),
    mergeMap(({ payload }) => of(
      enqueue(loadingScreen(signIn.type)),
      signInPost({ payload })
    ))
  ));

  public readonly signInPost$ = createEffect(() => this.actions$.pipe(
    ofType(signInPost),
    mergeMap(({ payload }) => this.signInClient.signIn(payload).pipe(
      mergeMap((payload) => of(
        authenticate({ payload }),
        dequeue(loadingScreen(signIn.type)),
        signInRedirect()
      )),
      catchErrorResponseWithConflictStatusCode((response) => of(
        signInPostError({ payload: response.error }),
        dequeue(loadingScreen(signIn.type))
      ))
    ))
  ));

  public readonly signInRedirect$ = createEffect(() => this.actions$.pipe(
    ofType(signInRedirect),
    concatLatestFrom(() => this.store.select(redirectUrl)),
    tap(([ _, redirectUrl ]) => this.router.navigateByUrl(redirectUrl || '/'))
  ),
  { dispatch: false })
}
