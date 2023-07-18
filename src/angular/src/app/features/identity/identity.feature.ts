import { dispose, init } from "./identity.actions";
import { IdentityState } from "./identity.state";
import { Injectable } from "@angular/core";
import { entities, permissions, state, user } from "./identity.selectors";
import { StaticFeature } from "../feature.abstractions";
import { Store } from "@ngrx/store";
import { filter, map } from "rxjs";
import { concatLatestFrom } from "@ngrx/effects";

@Injectable()
export class IdentityFeature extends StaticFeature<IdentityState> {

  constructor(store: Store) {
    super(store, state, init, dispose);
  }
  public readonly entities$ = this.createObservableOf(entities);

  public readonly permissions$ = this.createObservableOf(permissions);

  public readonly user$ = this.createObservableOf(user);

  public readonly anonymous$ = this.user$.pipe(
    map(user => !user)
  );

  public readonly authorize$ = (permissions: string[]) => this.permissions$.pipe(
    concatLatestFrom(() => this.entities$),
    map(([ permissions, entities]) => ({
      permissions: permissions || [],
      entities: entities || []
    })),
    map(({ permissions, entities }) => ([
      ...permissions.map(p => p.name),
      ...entities.reduce(
        (pv, nv) => [ ...pv, ...nv.permissions.map(p => p.name) ],
        [] as string[])
    ])),
    map((permissionNames) => ({
      claiming: [ ... (new Set(permissionNames)) ],
      requirements: [ ... (new Set(permissions)) ]
    })),
    map(({ claiming, requirements }) => ({
      claiming: claiming.filter(c => requirements.includes(c)),
      requirements
    })),
    map(({ claiming, requirements }) => claiming.length === requirements.length)
  );
}
