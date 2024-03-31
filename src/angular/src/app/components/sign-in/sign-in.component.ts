import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { SignInError, SignInForm } from "../../features/sign-in/sign-in-feature.abstractions";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";
import { FormGroupComponent } from "../form-group/form-group.component";

@Component({
  imports: [
    FormGroupComponent,
    ReactiveFormsModule
  ],
  selector: 'app-sign-in',
  standalone: true,
  templateUrl: './sign-in.component.html'
})
export class SignInComponent {

  // #region State

  private _passwordVisible: boolean = false;

  // #endregion

  // #region State accessors

  public get error(): SignInError | null {
    return this._signIn.error;
  }

  public get form(): SignInForm {
    return this._signIn.form;
  }

  public get passwordVisible(): boolean {
    return this._passwordVisible;
  }

  // #endregion

  // #region Actions

  public togglePasswordVisible(): void {
    this._passwordVisible = !this._passwordVisible;
  }

  public async submit(): Promise<boolean> {
    return this._signIn.submit();
  }

  // #endregion

  constructor(
    private readonly _signIn: SignInFeature
  ) {}
}
