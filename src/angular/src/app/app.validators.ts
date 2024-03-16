import { AbstractControl, Validators as CoreValidators, ValidationErrors, ValidatorFn } from "@angular/forms";
import { isLetter } from "./helpers/string.helper";

const DECIMAL_REGEX = /^\-?(\d+|(\d*(\.\d+)))$/;

const DECIMAL_UNSIGNED_REGEX = /^(\d+|(\d*(\.\d+)))$/;

const IBAN_REGEX = /^[A-Z]{2}\d{2}[A-Z0-9]+$/;

const INTEGER_REGEX = /^\-?\d+$/;

const INTEGER_UNSIGNED_REGEX = /^\-?\d+$/;

const PHONE_NUMBER_REGEX = /^((\+(?!351)\d+)|((2|9)\d{8}))$/;

const TAX_NUMBER_REGEX = /^(((?!PT)([A-Z]{2})\d+)|(\d{9}))$/;

const YEAR_REGEX = /^\d{4}$/;

export class Validators extends CoreValidators {

  static decimal(options?: { unsigned: boolean, precision: number }): ValidatorFn {
    return ((control: AbstractControl<string | number | null>) => {

      if (control.value) {

        const unsigned = options?.unsigned ?? false;
        const precision = options?.precision;

        const pattern = unsigned ? DECIMAL_UNSIGNED_REGEX : DECIMAL_REGEX;

        const valueAsString = 'string' === typeof(control.value)
          ? control.value
          : control.value.toString();

        if (!pattern.test(valueAsString))
          return ({ decimal: { unsigned, precision } });

        let decimals: string | undefined;

        if (precision && precision < (valueAsString.split('.')[1] ?? '').length)
          return ({ decimal: { unsigned, precision } });
      }

      return null;
    });
  }

  static iban(control: AbstractControl<string | null>): ValidationErrors | null {

    if (!control.value)
      return null;

    if (IBAN_REGEX.test(control.value)) {

      const shift = 55;
      const iban = control.value.toUpperCase();

      let inverse = iban.substring(4) + iban.substring(0, 4);
      let str = '';

      for (const chr of inverse.split('')) {

        let cv = isLetter(chr)
          ? (chr.charCodeAt(0) - shift)
          : (parseInt(chr));

        str += cv.toString();
      }

      let i;
      let cm = parseInt(str.charAt(0));

      for (i = 1; i < str.length; i++) {
          cm *= 10;
          cm += parseInt(str.charAt(i));
          cm %= 97;
      }

      if (cm === 1)
        return null;
    }

    return ({ iban: true });
  }

  static integer(options?: { unsigned: boolean }): ValidatorFn {
    return ((control: AbstractControl<string | null>) => {

      if (control.value) {
        const unsigned = options?.unsigned ?? false;
        const pattern = unsigned ? INTEGER_UNSIGNED_REGEX : INTEGER_REGEX;

        if (!pattern.test(control.value))
          return ({ integer: true, unsigned });
      }

      return null;
    });
  }

  static integerBetween(minimum: number, maximum: number): ValidatorFn {
    return ((control: AbstractControl<string | number | null>) => {

      if (control.value) {

        const valueAsString = 'string' === typeof(control.value)
          ? control.value
          : control.value.toString();

        if (!INTEGER_REGEX.test(valueAsString))
          return ({ between: { minimum, maximum }});

        const valueAsNumber = parseInt(valueAsString);

        if (valueAsNumber < minimum || valueAsNumber > maximum)
          return ({ between: { minimum, maximum }});
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
