import { accessTokenStorageKey, refreshTokenStorageKey } from "./identity.constants";
import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { authenticate, authenticateConfigureSecrets, authenticateRefresh, authenticateWriteSecrets, init, initClearSecrets, initConfigureWithSecrets, initConfigureWithoutSecrets, initReadSecrets, initRefresh, redirectToSignIn, refresh, refreshConfigureSecrets, refreshRefresh, refreshWriteSecrets, scheduleRefresh } from "./identity.actions";
import { catchErrorResponseWithConflictStatusCode, catchErrorResponseWithStatusCode } from "../../app.operators";
import { IdentityClient } from "./identity.client";
import { Injectable } from "@angular/core";
import { delay, map, mergeMap, of, tap } from "rxjs";
import { secretsAccessToken, secretsPersistent, secretsRefreshToken } from "./identity.selectors";
import { Store } from "@ngrx/store";
import { ActivatedRoute, Router } from "@angular/router";
import { createSignInUrl } from "../../containers/sign-in/sign-in.factories";

const numberOfSecondsForRefreshBeforeAccessTokenExpires = 5;

@Injectable()
export class IdentityEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly activatedRoute: ActivatedRoute,
    private readonly identityClient: IdentityClient,
    private readonly router: Router,
    private readonly store: Store
  ) { }

  public readonly authenticate$ = createEffect(() => this.actions$.pipe(
    ofType(authenticate),
    map(({ payload }) => authenticateRefresh({ payload }))
  ));

  public readonly authenticateRefresh$ = createEffect(() => this.actions$.pipe(
    ofType(authenticateRefresh),
    mergeMap((action) => this.identityClient.refresh(action.payload.accessToken, {
      refreshToken: action.payload.refreshToken
    }).pipe(
      map((response) => authenticateWriteSecrets({
        payload: {
          ...response,
          persistent: action.payload.persistent
        }
      }))
    ))
  ));

  public readonly authenticateWriteSecrets$ = createEffect(() => this.actions$.pipe(
    ofType(authenticateWriteSecrets),
    tap(({ payload }) => {
      const storage = payload.persistent ? localStorage :
        sessionStorage;

      storage.setItem(accessTokenStorageKey, payload.accessToken);
      storage.setItem(refreshTokenStorageKey, payload.refreshToken);
    }),
    map(({ payload }) => authenticateConfigureSecrets({ payload }))
  ));

  public readonly authenticateConfigureSecrets$ = createEffect(() => this.actions$.pipe(
    ofType(authenticateConfigureSecrets),
    map(({ payload }) => scheduleRefresh({
      payload: {
        accessTokenExpiration: payload.accessTokenExpiration
      }
    }))
  ));

  public readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    map(() => initReadSecrets())
  ));

  public readonly initSecrets$ = createEffect(() => this.actions$.pipe(
    ofType(initReadSecrets),
    map(() => {

      for (const storage of [localStorage, sessionStorage]) {

        const accessToken = storage.getItem(accessTokenStorageKey);

        if (!accessToken)
          continue;

        const refreshToken = storage.getItem(refreshTokenStorageKey);

        if (!refreshToken)
          continue;

        const payload = {
          accessToken,
          refreshToken,
          persistent: (storage === localStorage)
        };

        return initRefresh({ payload });
      }

      return initConfigureWithoutSecrets();
    })
  ));

  public readonly initRefresh$ = createEffect(() => this.actions$.pipe(
    ofType(initRefresh),
    mergeMap((action) => this.identityClient.refresh(action.payload.accessToken, {
      refreshToken: action.payload.refreshToken
    }).pipe(
      mergeMap((response) => [
        initConfigureWithSecrets({
          payload: {
            ...response,
            persistent: action.payload.persistent
          }
        }),
        scheduleRefresh({
          payload: {
            accessTokenExpiration: response.accessTokenExpiration
          }
        })
      ])
    )),
    catchErrorResponseWithStatusCode([ 401, 403, 409 ], () => of(
      initClearSecrets()
    ))
  ));

  public readonly initClearSecrets$ = createEffect(() => this.actions$.pipe(
    ofType(initClearSecrets),
    tap(() => {
      for (const storage of [ localStorage, sessionStorage ]) {
        storage.removeItem(accessTokenStorageKey);
        storage.removeItem(refreshTokenStorageKey);
      }
    }),
    map(() => initConfigureWithoutSecrets())
  ));

  public readonly redirectToSignIn$ = createEffect(() => this.actions$.pipe(
    ofType(redirectToSignIn),
    tap(() => this.router.navigateByUrl(createSignInUrl(this.router.url)))
  ),
  { dispatch: false })

  public readonly refresh$ = createEffect(() => this.actions$.pipe(
    ofType(refresh),
    concatLatestFrom(() => [
      this.store.select(secretsAccessToken),
      this.store.select(secretsRefreshToken)
    ]),
    map(([ _, accessToken, refreshToken ]) => refreshRefresh({
      payload: {
        accessToken: accessToken!,
        refreshToken: refreshToken!
      }
    }))
  ));

  public readonly refreshRefresh$ = createEffect(() => this.actions$.pipe(
    ofType(refreshRefresh),
    mergeMap(({ payload }) => this.identityClient.refresh(payload.accessToken, {
      refreshToken: payload.refreshToken
    }).pipe(
      map((response) => refreshWriteSecrets({
        payload: {
          ...response
        }
      }))
    )),
    catchErrorResponseWithConflictStatusCode((response) => of(
      redirectToSignIn()
    ))
  ));

  public readonly refreshWriteSecrets$ = createEffect(() => this.actions$.pipe(
    ofType(refreshWriteSecrets),
    concatLatestFrom(() => this.store.select(secretsPersistent)),
    tap(([ { payload }, persistent ]) => {
      const storage = persistent ? localStorage :
        sessionStorage;

      storage.setItem(accessTokenStorageKey, payload.accessToken);
      storage.setItem(refreshTokenStorageKey, payload.refreshToken);
    }),
    mergeMap(([{ payload }]) => [
      refreshConfigureSecrets({ payload }),
      scheduleRefresh({
        payload: {
          accessTokenExpiration: payload.accessTokenExpiration
        }
      })
    ])
  ));

  public readonly scheduleRefresh$ = createEffect(() => this.actions$.pipe(
    ofType(scheduleRefresh),
    mergeMap(({ payload }) => of(payload.accessTokenExpiration).pipe(
      delay(1000 * (payload.accessTokenExpiration - numberOfSecondsForRefreshBeforeAccessTokenExpires))
    )),
    map(() => refresh())
  ));
}
