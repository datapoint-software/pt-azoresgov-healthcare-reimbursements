import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { conflict } from "@app/api/api.helpers";
import { GenericSignInFeatureClient } from "@app/api/generic-sign-in-feature/generic-sign-in-feature.client";
import { CoreIdentityFeature } from "@app/features/core-identity/core-identity.feature";
import { CoreLoadingOverlayFeature } from "@app/features/core-loading-overlay/core-loading-overlay.feature";
import { Feature } from "@app/features/feature.abstractions";
import { GenericSignInFeatureError, GenericSignInFeatureForm } from "@app/features/generic-sign-in/generic-sign-in-feature.abstractions";

@Injectable()
export class GenericSignInFeature implements Feature {

  // #region State

  private _error: GenericSignInFeatureError | null = undefined!;

  private _form: GenericSignInFeatureForm = undefined!;

  private _persistentSessionsEnabled: boolean = undefined!;

  private _redirectUrl: string = undefined!;

  // #endregion

  // #region State accessors

  public get error(): GenericSignInFeatureError | null {
    return this._error;
  }

  public get form(): GenericSignInFeatureForm {
    return this._form;
  }

  public get persistentSessionsEnabled(): boolean {
    return this._persistentSessionsEnabled;
  }

  public get redirectUrl(): string {
    return this._redirectUrl;
  }

  // #endregion

  // #region Actions

  public async init(redirectUrl: string | null): Promise<void> {

    const options = await this._client.getOptions();

    this._error = null;

    this._form = this._fb.group({
      emailAddress: this._fb.control('', [ Validators.required, Validators.email, Validators.maxLength(128) ]),
      password: this._fb.control('', [ Validators.required, Validators.maxLength(1024) ]),
      persistent: this._fb.control(false, [ ])
    });

    this._persistentSessionsEnabled = options.persistentSessionsEnabled;

    this._redirectUrl = redirectUrl ?? '/';

    if (!this._persistentSessionsEnabled)
      this._form.controls.persistent.disable();
  }

  public async submit(): Promise<boolean> {

    this._loadingOverlayFeature.enqueue(GenericSignInFeature.name);

    const claims = await this._client.signIn({
      emailAddress: this._form.value.emailAddress!,
      password: this._form.value.password!,
      persistent: this._form.value.persistent ?? false
    }).catch(conflict(({ message }) => {
      this._error = ({ message });
      this._loadingOverlayFeature.dequeue(GenericSignInFeature.name);
      return null;
    }));

    if (!claims)
      return false;

    this._identityFeature.authenticate({
      ...claims,
      expiration: (claims.expiration && new Date(claims.expiration)) || null
    });

    if (!await this._router.navigateByUrl(this._redirectUrl))
      await this._router.navigateByUrl('/');

    this._loadingOverlayFeature.dequeue(GenericSignInFeature.name);

    return true;
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _client: GenericSignInFeatureClient,
    private readonly _identityFeature: CoreIdentityFeature,
    private readonly _loadingOverlayFeature: CoreLoadingOverlayFeature,
    private readonly _router: Router
  ) {}
}
