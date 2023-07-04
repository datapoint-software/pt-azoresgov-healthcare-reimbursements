import { Injectable } from "@angular/core";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { map, mergeMap, of, tap, withLatestFrom } from "rxjs";
import { SignInClient } from "./sign-in.client";
import { SignInFeature } from "./sign-in.feature";
import { mergeLoadingOverlay } from "../loading-overlay/loading-overlay.operators";
import { ActivatedRoute, Router } from "@angular/router";
import { catchErrorResponseWithConflictStatusCode } from "../../app.operators";

@Injectable()
export class SignInEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly activatedRoute: ActivatedRoute,
    private readonly client: SignInClient,
    private readonly feature: SignInFeature,
    private readonly router: Router
  ) {}

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(this.feature.init$$),
    mergeMap(() => this.client.getOptions()),
    map((response) => this.feature.configure$$({
      payload: {
        authentication: {
          enabled: response.authentication.enabled,
          persistentEnabled: response.authentication.persistentEnabled
        }
      }
    }))
  ));

  public readonly redirect$ = createEffect(() => this.actions$.pipe(
    ofType(this.feature.redirect$$),
    mergeMap((action) => of(action).pipe(
      withLatestFrom(this.activatedRoute.queryParams)
    )),
    tap(([ _, queryParams]) => {
      this.router.navigateByUrl(queryParams['forward'] || '/')
    })
  ),
  { dispatch: false });

  public readonly signIn$ = createEffect(() => this.actions$.pipe(
    ofType(this.feature.signIn$$),
    mergeLoadingOverlay((action) => of(action).pipe(
      mergeMap(() => this.client.signIn({
        emailAddress: action.payload.emailAddress,
        password: action.payload.password
      })),
      map((_) => this.feature.redirect$$()),
      catchErrorResponseWithConflictStatusCode((response) => of(
        this.feature.error$$({ payload: response.error })
      ))
    ))
  ));

}
