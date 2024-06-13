import { Injectable } from "@angular/core";
import { CoreIdentityFeatureClient } from "@app/api/core-identity-feature/core-identity-feature.client";
import { CoreIdentityFeatureClaims } from "@app/features/core-identity/core-identity-feature.abstractions";
import { UserRoleNature } from "@app/enums";
import { conflict, unauthorized } from "@app/api/api.helpers";
import { Feature } from "@app/features/feature.abstractions";
import { APP_IDENTITY_REFRESH_TIMEOUT_BUFFER } from "@app/constants";

@Injectable()
export class CoreIdentityFeature implements Feature {

  // #region State

  private _claims: CoreIdentityFeatureClaims | null = null;

  private _refreshTimeoutId: ReturnType<typeof setTimeout> | null = null;

  // #endregion

  // #region State accessors

  public get administrative(): boolean {
    return -1 < (this._claims?.roles.indexOf(UserRoleNature.Administrative) ?? -1);
  }

  public get anonymous(): boolean {
    return this._claims === null;
  }

  public get authenticated(): boolean {
    return !!this._claims;
  }

  public get claims(): CoreIdentityFeatureClaims {

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

  public authenticate(claims: CoreIdentityFeatureClaims): void {
    this._claims = claims;
  }

  public async refresh(): Promise<void> {

    if (this._refreshTimeoutId)
      clearTimeout(this._refreshTimeoutId);

    const claims = await this._client.refresh()
      .catch(conflict((_) => null))
      .catch(unauthorized((_) => null));

    this._claims = claims && ({
      ...claims,
      expiration: (claims.expiration && new Date(claims.expiration)) || null
    });

    if (this._claims?.expiration) {
      this._refreshTimeoutId = setTimeout(
        () => this.refresh(),
        ((this._claims.expiration.getTime() - new Date().getTime()) - APP_IDENTITY_REFRESH_TIMEOUT_BUFFER)
      );
    }
  }

  // #endregion

  constructor(
    private _client: CoreIdentityFeatureClient
  ) {}
}
