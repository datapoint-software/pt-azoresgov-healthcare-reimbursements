import { Injectable } from "@angular/core";
import { Feature } from "../feature.abstract";
import { IdentityFeatureClaims } from "./identity-feature.abstractions";
import { IdentityFeatureClient } from "../../api/features/identity/identity-feature.client";
import { conflict, forbidden, unauthorized } from "../../api/api.helpers";

@Injectable()
export class IdentityFeature implements Feature {

  // #region State

  private _claims: IdentityFeatureClaims | null = null;

  // #endregion

  // #region State accessors

  public get anonymous(): boolean {
    return this._claims === null;
  }

  public get authenticated(): boolean {
    return !!this._claims;
  }

  public get claims(): IdentityFeatureClaims {

    if (!this._claims)
      throw new Error("Can not get claims for an anonymous identity.");

    return this._claims;
  }

  public get emailAddress(): string {
    return this.emailAddress;
  }

  public get expiration(): Date | null {
    return this.claims.expiration;
  }

  public get id(): string {
    return this.claims.id;
  }

  public get name(): string {
    return this.claims.name;
  }

  public get rowVersionId(): string {
    return this.claims.rowVersionId;
  }

  // #endregion

  // #region Actions

  public authenticate(claims: IdentityFeatureClaims): void {
    this._claims = claims;
  }

  public async refresh(): Promise<void> {

    const claims = await this._client.refresh()
      .catch(conflict((_) => null))
      .catch(unauthorized((_) => null));

    this._claims = claims && ({
      ...claims,
      expiration: (claims.expiration && new Date(claims.expiration)) || null
    });
  }

  // #endregion

  constructor(
    private _client: IdentityFeatureClient
  ) {}
}
