import { Component } from "@angular/core";
import { ReactiveFormsModule } from "@angular/forms";
import { SuiFormGroupComponent } from "@app/components/sui-form-group/sui-form-group.component";
import { GenericSignInFeatureError, GenericSignInFeatureForm } from "@app/features/generic-sign-in/generic-sign-in-feature.abstractions";
import { GenericSignInFeature } from "@app/features/generic-sign-in/generic-sign-in.feature";

@Component({
  imports: [
    SuiFormGroupComponent,
    ReactiveFormsModule
  ],
  selector: 'app-generic-sign-in',
  standalone: true,
  templateUrl: 'generic-sign-in.component.html'
})
export class GenericSignInComponent {

  // #region State

  private _passwordVisible: boolean = false;

  // #endregion

  // #region State accessors

  public get error(): GenericSignInFeatureError | null {
    return this._signIn.error;
  }

  public get form(): GenericSignInFeatureForm {
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
    private readonly _signIn: GenericSignInFeature
  ) {}
}
