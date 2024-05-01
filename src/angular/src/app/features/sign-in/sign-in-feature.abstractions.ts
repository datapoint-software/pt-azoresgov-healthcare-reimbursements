import { FormControl, FormGroup } from "@angular/forms";

export type SignInFeatureError = {
  message: string;
};

export type SignInFeatureForm = FormGroup<{
  emailAddress: FormControl<string | null>;
  password: FormControl<string | null>;
  persistent: FormControl<boolean | null>;
}>;
