import { AbstractControl, ValidationErrors, Validators } from "@angular/forms";

export class AppValidators extends Validators {

  public static patientNumber(control: AbstractControl<string | null>): ValidationErrors | null {

    if (control.invalid || !control.value)
      return null;

    if ((/^\d{9}$/).test(control.value))
      return null;

    return ({ patientnumber: true });
  }

  public static taxNumber(control: AbstractControl<string | null>): ValidationErrors | null {

    if (control.invalid || !control.value)
      return null;

    if ((/^\d{9}$/).test(control.value))
      return null;

    return ({ taxnumber: true });
  }

  private constructor() { super(); }
}
