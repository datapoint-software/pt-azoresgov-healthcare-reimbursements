import { Pipe, PipeTransform } from "@angular/core";
import { ProcessStatus } from "../enums/process-status.enum";

@Pipe({
  standalone: true,
  name: 'processStatus'
})
export class ProcessStatusPipe implements PipeTransform {

  transform(status: ProcessStatus) {
    return (
      status === ProcessStatus.Capture ? 'Registo' :
      status === ProcessStatus.DocumentUpload ? 'Carregamento de documentos' :
      status === ProcessStatus.Codification ? 'Codificação' :
      status === ProcessStatus.Payment ? 'Pagamento' :
      status === ProcessStatus.Validation ? 'Validação' :
      status === ProcessStatus.Cancelled ? 'Cancelado' :
        'Desconhecido'
    );
  }

}
