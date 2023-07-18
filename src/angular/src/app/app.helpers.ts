import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { concatLatestFrom } from "@ngrx/effects";
import { Observable, concatMap, from, last, map, mergeMap, of, skipWhile, takeUntil, takeWhile, tap } from "rxjs";
import { IdentityFeature } from "./features/identity/identity.feature";

export const authorize = (permissions: Array<string>): CanActivateFn => () => {

  const identity = inject(IdentityFeature);
  const router = inject(Router);

  return of(permissions).pipe(

    concatLatestFrom(() => identity.anonymous$),

    mergeMap(([ permissions, anonymous ]) => {

      if (anonymous) {
        return of(router.createUrlTree([ '/sign-in' ], {
          queryParams: {
            forward: document.location.pathname
          }
        }));
      }

      return of(permissions).pipe(

        concatLatestFrom(() => identity.authorize$(permissions)),

        map(([ _, result ]) => result ? result : router.createUrlTree([ '/error' ], {
          queryParams: {
            status: 403,
            message: 'TiVDMyVBM28lMjB0ZW0lMjBwZXJtaXNzJUMzJUI1ZXMlMjBzdWZpY2llbnRlcyUyMHBhcmElMjBhY2VkZXIlMjBhJTIwZXN0YSUyMGZ1bmNpb25hbGlkYWRlLg=='
          }
        }))
      );
    })
  );
};

export const canActivateSequence = (canActivateFns: Array<CanActivateFn>) : CanActivateFn =>

    (route, state) => {

      const results = canActivateFns.map(fn => fn(route, state));

      if (results.length === 0)
        return true;

      return from(results).pipe(

        concatMap((result) => {

          if (result instanceof Observable || result instanceof Promise)
            return result;

          return of(result);
        }),

        takeWhile((result) => result === true, true),
        last()
      );
    };
