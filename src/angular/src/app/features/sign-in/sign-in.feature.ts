import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Feature } from "../feature.abstract";
import { SignInFeatureError, SignInFeatureForm } from "./sign-in-feature.abstractions";
import { SignInFeatureClient } from "../../api/features/sign-in/sign-in-feature.client";
import { IdentityFeature } from "../identity/identity.feature";
import { NEVER } from "rxjs";
import { conflict } from "../../api/api.helpers";
import { Router } from "@angular/router";
import { LoadingOverlayFeature } from "../loading-overlay/loading-overlay.feature";

@Injectable()
export class SignInFeature implements Feature {

  // #region State

  private _error: SignInFeatureError | null = undefined!;

  private _form: SignInFeatureForm = undefined!;

  private _persistentSessionsEnabled: boolean = undefined!;

  private _redirectUrl: string = undefined!;

  // #endregion

  // #region State accessors

  public get error(): SignInFeatureError | null {
    return this._error;
  }

  public get form(): SignInFeatureForm {
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

    this._loadingOverlayFeature.enqueue(SignInFeature.name);

    const claims = await this._client.signIn({
      emailAddress: this._form.value.emailAddress!,
      password: this._form.value.password!,
      persistent: this._form.value.persistent ?? false
    }).catch(conflict(({ message }) => {
      this._error = ({ message });
      this._loadingOverlayFeature.dequeue(SignInFeature.name);
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

    this._loadingOverlayFeature.dequeue(SignInFeature.name);

    return true;
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder,
    private readonly _client: SignInFeatureClient,
    private readonly _identityFeature: IdentityFeature,
    private readonly _loadingOverlayFeature: LoadingOverlayFeature,
    private readonly _router: Router
  ) {}
}
