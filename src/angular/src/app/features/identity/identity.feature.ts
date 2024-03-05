import { EnvironmentProviders, Injectable, makeEnvironmentProviders } from "@angular/core";
import { IdentityState } from "./rx/identity.state";
import { Feature } from "../feature.abstractions";
import { Store, provideState } from "@ngrx/store";
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot } from "@angular/router";
import { TypedAction } from "@ngrx/store/src/models";
import { state, user } from "./rx/identity.selectors";
import { dispose, init } from "./rx/identity.actions";
import { IdentityEffects } from "./rx/identity.effects";
import { provideEffects } from "@ngrx/effects";
import { FEATURE_NAME } from "./identity.constants";
import { reducer } from "./rx/identity.reducer";
import { firstValueFrom } from "rxjs";

@Injectable()
export class IdentityFeature extends Feature<IdentityState> {

  public readonly user$ = this.of(user);

  constructor(private readonly router: Router, store: Store) {
    super(store, state);
  }

  public async authorize(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot, roles?: Array<string>) {

    const user = await firstValueFrom(this.user$);

    if (!user) {
      return this.router.createUrlTree([ '/sign-in' ], {
        queryParams: {
          redirect: activatedRoute.url
        }
      });
    }

    return true;
  }

  protected override dispose$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return dispose();
  }

  protected override init$$$(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot): TypedAction<string> {
    return init();
  }
}

export const provideIdentityFeature = (): Array<EnvironmentProviders> => [

  makeEnvironmentProviders([
    IdentityFeature
  ]),

  provideEffects(IdentityEffects),
  provideState(FEATURE_NAME, reducer)
];
