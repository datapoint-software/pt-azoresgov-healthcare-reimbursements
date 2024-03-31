import { Injectable } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Feature } from "../feature.abstract";
import { SignInError, SignInForm } from "./sign-in-feature.abstractions";

@Injectable()
export class SignInFeature implements Feature {

  // #region State

  private _error: SignInError | null = undefined!;

  private _form: SignInForm = undefined!;

  private _persistentSessionsEnabled: boolean = undefined!;

  private _redirectUrl: string = undefined!;

  // #endregion

  // #region State accessors

  public get error(): SignInError | null {
    return this._error;
  }

  public get form(): SignInForm {
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

    this._error = null;

    this._form = this._fb.group({
      emailAddress: this._fb.control('', [ Validators.required, Validators.email, Validators.maxLength(128) ]),
      password: this._fb.control('', [ Validators.required, Validators.maxLength(1024) ]),
      persistent: this._fb.control(false, [ ])
    });

    this._persistentSessionsEnabled = false;

    this._redirectUrl = redirectUrl ?? '/';

    if (!this._persistentSessionsEnabled)
      this._form.controls.persistent.disable();
  }

  public async submit(): Promise<boolean> {
    return false;
  }

  // #endregion

  constructor(
    private readonly _fb: FormBuilder
  ) {}
}
