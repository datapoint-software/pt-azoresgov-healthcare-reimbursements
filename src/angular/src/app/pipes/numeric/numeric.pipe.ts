import { Pipe, PipeTransform } from "@angular/core";
import { isNumber } from "@app/helpers";

@Pipe({
  name: "numeric",
  standalone: true
})
export class NumericPipe implements PipeTransform {

  transform(value: number | string) {
    const number = isNumber(value) ? value : parseFloat(value);
    return number.toLocaleString('pt-PT');
  }
}
