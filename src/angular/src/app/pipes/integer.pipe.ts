import { Pipe, PipeTransform } from "@angular/core";
import { APP_LOCALE } from "../app.constants";

@Pipe({
  standalone: true,
  name: 'integer'
})
export class IntegerPipe implements PipeTransform {

  transform(value: number | string | null, ...args: any[]) {

    if (value === null)
      return null;

    if ('string' === typeof(value))
      value = parseInt(value);

    return value.toLocaleString(APP_LOCALE);
  }
}
