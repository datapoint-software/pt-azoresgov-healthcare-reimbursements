import { FormControl, FormGroup } from "@angular/forms";

export type GenericSignInFeatureError = {
  message: string;
};

export type GenericSignInFeatureForm = FormGroup<{
  emailAddress: FormControl<string | null>;
  password: FormControl<string | null>;
  persistent: FormControl<boolean | null>;
}>;
