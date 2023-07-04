import { CommonModule } from "@angular/common";
import { Component, EventEmitter, Input, Output } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { SignInFormSubmitEvent } from "./sign-in-form.events";
import { ErrorModel } from "../../app.models";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-sign-in-form',
  standalone: true,
  templateUrl: './sign-in-form.component.html'
})
export class SignInFormComponent {

  @Input()
  public error?: ErrorModel;

  @Input()
  public persistentEnabled: boolean = false;

  @Output()
  public readonly submit = new EventEmitter<SignInFormSubmitEvent>();

  public readonly form = new FormGroup({
    emailAddress: new FormControl('', [ Validators.required, Validators.maxLength(256), Validators.email ]),
    password: new FormControl('', [ Validators.required, Validators.maxLength(1024) ]),
    persistent: new FormControl(false, [])
  });

  public isPasswordVisible: boolean = false;

  onPasswordButtonClick(e: Event) {

    e.preventDefault();
    e.stopImmediatePropagation();

    this.isPasswordVisible = !this.isPasswordVisible;
  }

  onSubmit(e: Event) {

    e.preventDefault();
    e.stopImmediatePropagation();

    if (this.form.invalid)
      return;

    this.submit.emit(new SignInFormSubmitEvent({
      emailAddress: this.form.value.emailAddress as string,
      password: this.form.value.password as string,
      persistent: this.form.value.persistent as boolean
    }));
  }
}
