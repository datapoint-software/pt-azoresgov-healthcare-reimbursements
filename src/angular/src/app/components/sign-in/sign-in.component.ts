import { AuthenticationMethod } from "../../enums/authentication-method.enum";
import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { EnvironmentFeature } from "../../features/environment/environment.feature";
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from "@angular/forms";
import { map } from "rxjs";
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
export class SignInComponent {

  constructor(
    private readonly environment: EnvironmentFeature,
    private readonly signIn: SignInFeature
  ) {}

  readonly environmentProductVersion$ = this.environment.productVersion$;

  readonly signInBasicMethodPersistentSessionsEnabled$ = this.signIn.basicMethodPersistentSessionsEnabled$;

  readonly signInError$ = this.signIn.error$;

  readonly signInMethodIsBasic$ = this.signIn.method$
    .pipe(map((method) => method === AuthenticationMethod.Basic));

  readonly form = new FormGroup({
    emailAddress: new FormControl('', [ Validators.required, Validators.maxLength(256), Validators.email ]),
    password: new FormControl('', [ Validators.required, Validators.maxLength(1024) ]),
    persistent: new FormControl(false)
  });

  isPasswordVisible: boolean = false;

  onPasswordButtonClick(e: Event) {

    e.preventDefault();
    e.stopPropagation();

    this.isPasswordVisible = !this.isPasswordVisible;
  }

  onSubmit(e: Event) {

    e.preventDefault();
    e.stopPropagation();

    if (this.form.invalid)
      return;

    this.signIn.signIn(
      this.form.value.emailAddress!,
      this.form.value.password!,
      this.form.value.persistent!
    );
  }
}
