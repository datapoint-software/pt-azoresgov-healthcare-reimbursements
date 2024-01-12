import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";
import { RouterModule } from "@angular/router";
import { SignInFormComponent } from "../../components/sign-in-form/sign-in-form.component";
import { SignInFormSubmitEvent } from "../../components/sign-in-form/sign-in-form.events";
import { EnvironmentFeature } from "../../features/environment/environment.feature";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    SignInFormComponent
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

  public readonly authenticationPersistentEnabled$ = this.signIn.authenticationPersistentEnabled$;
  public readonly authenticationError$ = this.signIn.authenticationError$;
  public readonly debugSymbols$ = this.environment.debugSymbols$;
  public readonly development$ = this.environment.development$;
  public readonly productVersion$ = this.environment.productVersion$;

  public readonly form = new FormGroup({
    emailAddress: new FormControl('', [ Validators.required, Validators.maxLength(256), Validators.email ]),
    password: new FormControl('', [ Validators.required, Validators.maxLength(1024) ]),
    persistent: new FormControl(false)
  });

  onSubmit(e: SignInFormSubmitEvent) {

    e.preventDefault();
    e.stopImmediatePropagation();

    this.signIn.signIn({
      emailAddress: e.payload.emailAddress,
      password: e.payload.password,
      persistent: e.payload.persistent
    });
  }
}
