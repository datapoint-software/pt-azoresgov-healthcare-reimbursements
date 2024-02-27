import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { AuthenticationMethod } from "../../../enums/authentication-method.enum";
import { catchConflict } from "../../feature-rx.helpers";
import { configure, error, init, redirect, signIn } from "./sign-in.actions";
import { Injectable } from "@angular/core";
import { map, mergeMap, of, tap } from "rxjs";
import { mergeLoadingOverlay } from "../../loading-overlay/rx/loading-overlay.operators";
import { redirectUrl } from "./sign-in.selectors";
import { Router } from "@angular/router";
import { SignInClient } from "../../../clients/sign-in/sign-in.client";
import { Store } from "@ngrx/store";

import * as identity from '../../identity/rx/identity.actions';

@Injectable()
export class SignInEffects {
  constructor(
    private readonly actions$: Actions,
    private readonly router: Router,
    private readonly signInClient: SignInClient,
    private readonly store: Store
  ) {}

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(() => this.signInClient.getOptions().pipe(
      map((response) => configure({
        payload: {
          ...response,
          method: AuthenticationMethod.Basic
        }
      }))
    ))
  ));

  readonly redirect$ = createEffect(() => this.actions$.pipe(
    ofType(redirect),
    concatLatestFrom(() => this.store.select(redirectUrl)),
    tap(([ _, redirectUrl ]) => this.router.navigateByUrl(redirectUrl || '/'))
  ),

  { dispatch: false })

  readonly signIn$ = createEffect(() => this.actions$.pipe(
    ofType(signIn),
    mergeLoadingOverlay((action) => of(action).pipe(
      mergeMap(({ payload }) => this.signInClient.signIn({ ...payload }).pipe(
        mergeMap((result) => of(
          identity.configure({
            payload: {
              user: { ...result.user }
            }
          }),
          redirect()
        )),
        catchConflict((response) => of(error({ payload: response })))
      ))
    ))
  ));
}
