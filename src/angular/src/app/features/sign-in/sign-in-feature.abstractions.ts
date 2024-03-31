import { FormControl, FormGroup } from "@angular/forms";

export type SignInError = {
  message: string;
};

export type SignInForm = FormGroup<{
  emailAddress: FormControl<string | null>;
  password: FormControl<string | null>;
  persistent: FormControl<boolean | null>;
}>;
