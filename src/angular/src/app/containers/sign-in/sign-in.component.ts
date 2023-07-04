import { CommonModule } from "@angular/common";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ErrorModel } from "../../app.models";
import { SignInFeature } from "../../features/sign-in/sign-in.feature";
import { takeUntil } from "rxjs";
import { RouterModule } from "@angular/router";
import { SignInFormComponent } from "../../components/sign-in-form/sign-in-form.component";
import { SignInFormSubmitEvent } from "../../components/sign-in-form/sign-in-form.events";

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
export class SignInComponent implements OnInit {

  constructor(
    private readonly signInFeature: SignInFeature
  ) {}

  public persistentEnabled: boolean = false;

  public error?: ErrorModel;

  public readonly form = new FormGroup({
    emailAddress: new FormControl('', [ Validators.required, Validators.maxLength(256), Validators.email ]),
    password: new FormControl('', [ Validators.required, Validators.maxLength(1024) ]),
    persistent: new FormControl(false)
  });

  onSubmit(e: SignInFormSubmitEvent) {

    e.preventDefault();
    e.stopImmediatePropagation();

    this.signInFeature.signIn({
      emailAddress: e.payload.emailAddress,
      password: e.payload.password,
      persistent: e.payload.persistent
    });
  }

  ngOnInit() {

    const disposing$ = this.signInFeature.disposing$;

    this.signInFeature.authenticationPersistentEnabled$
      .pipe(takeUntil(disposing$))
      .subscribe((persistentEnabled) => this.persistentEnabled = persistentEnabled);

    this.signInFeature.error$
      .pipe(takeUntil(disposing$))
      .subscribe((error) => this.error = error);
  }

}
