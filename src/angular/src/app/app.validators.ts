import { AbstractControl, Validators as CoreValidators, ValidationErrors, ValidatorFn } from "@angular/forms";

const DECIMAL_REGEX = /^\-?(\d+|(\d*(\.\d+)))$/;

const DECIMAL_UNSIGNED_REGEX = /^(\d+|(\d*(\.\d+)))$/;

const INTEGER_REGEX = /^\-?\d+$/;

const INTEGER_UNSIGNED_REGEX = /^\-?\d+$/;

const PHONE_NUMBER_REGEX = /^((\+(?!351)\d+)|((2|9)\d{8}))$/;

const TAX_NUMBER_REGEX = /^(((?!PT)([A-Z]{2})\d+)|(\d{9}))$/;

const YEAR_REGEX = /^\d{4}$/;

export class Validators extends CoreValidators {

  static decimal(options?: { unsigned: boolean }): ValidatorFn {
    return ((control: AbstractControl<string | null>) => {

      if (control.value)
      {
        const unsigned = options?.unsigned ?? false;
        const pattern = unsigned ? DECIMAL_UNSIGNED_REGEX : DECIMAL_REGEX;

        if (!pattern.test(control.value))
          return ({ decimal: true, unsigned });
      }

      return null;
    });
  }

  static integer(options?: { unsigned: boolean }): ValidatorFn {
    return ((control: AbstractControl<string | null>) => {

      if (control.value)
      {
        const unsigned = options?.unsigned ?? false;
        const pattern = unsigned ? INTEGER_UNSIGNED_REGEX : INTEGER_REGEX;

        if (!pattern.test(control.value))
          return ({ integer: true, unsigned });
      }

      return null;
    });
  }

  static phoneNumber(control: AbstractControl<string | null>): ValidationErrors | null {

    if (!control.value || PHONE_NUMBER_REGEX.test(control.value))
      return null;

    return ({ phonenumber: true });
  }

  static taxNumber(control: AbstractControl<string | null>): ValidationErrors | null {

    if (!control.value)
      return null;

    if (TAX_NUMBER_REGEX.test(control.value)) {

      let taxNumber = control.value;

      if (taxNumber.length !== 9)
        return null;

      if (/^[123]|45|5/.test(taxNumber)) {

        const digits = taxNumber
          .split('')
          .map((d) => parseInt(d));

        const sum =
          digits[0] * 9 +
          digits[1] * 8 +
          digits[2] * 7 +
          digits[3] * 6 +
          digits[4] * 5 +
          digits[5] * 4 +
          digits[6] * 3 +
          digits[7] * 2;

        const div = Math.floor(sum / 11);
        const mod = sum - (div * 11);
        const cd = mod == 1 || mod == 0 ? 0 : 11 - mod;

        if (digits[8] == cd)
          return null;
      }
    }

    return ({ taxnumber: true });
  }

  static year(control: AbstractControl<string | null>): ValidationErrors | null {

    if (control.value && !YEAR_REGEX.test(control.value))
      return ({ year: true });

    return null;
  }
}
