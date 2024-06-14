import { AbstractControl, ValidationErrors, Validators } from "@angular/forms";

export class AppValidators extends Validators {

  public static patientNumber(control: AbstractControl<string | null>): ValidationErrors | null {

    if (control.invalid || !control.value)
      return null;

    if ((/^\d{9}$/).test(control.value))
      return null;

    return ({ patientnumber: true });
  }

  private constructor() { super(); }
}
