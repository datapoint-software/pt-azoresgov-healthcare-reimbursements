import { CommonModule } from "@angular/common";
import { Component, OnDestroy } from "@angular/core";
import { EnvironmentFeature } from "../../features/environment/environment.feature";
import { ReactiveFormsModule } from "@angular/forms";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-sign-in',
  standalone: true,
  templateUrl: './sign-in.component.html'
})
export class SignInComponent implements OnDestroy {

  constructor(
    private readonly environment: EnvironmentFeature,
    private readonly signIn: SignInFeature
  ) {}

  readonly form = this.signIn.form;

  readonly environmentProductVersion$ = this.environment.productVersion$;

  readonly signInBasicMethodPersistentSessionsEnabled$ = this.signIn.basicMethodPersistentSessionsEnabled$;

  readonly signInBasicMethodVisible$ = this.signIn.basicMethodVisible$;

  readonly signInError$ = this.signIn.error$;

  isPasswordVisible: boolean = false;

  ngOnDestroy() {
    this.signIn.dispose();
  }

  onPasswordVisibleButtonClick() {
    this.isPasswordVisible = !this.isPasswordVisible;
  }

  onSubmit() {
    this.signIn.signIn();
  }
}
