import { Pipe, PipeTransform } from "@angular/core";
import { ProcessStatus } from "../enums/process-status.enum";

@Pipe({
  standalone: true,
  name: 'processStatus'
})
export class ProcessStatusPipe implements PipeTransform {

  transform(value: ProcessStatus) {
    return 'Raschunho';
  }

}
