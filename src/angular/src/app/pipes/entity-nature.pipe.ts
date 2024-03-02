import { Pipe, PipeTransform } from "@angular/core";
import { EntityNature } from "../enums/entity-nature.enum";

@Pipe({
  standalone: true,
  name: 'entityNature'
})
export class EntityNaturePipe implements PipeTransform {

  transform(value: EntityNature) {
    return value === EntityNature.Administrative ? 'Unidade de Saúde de Ilha' :
      value === EntityNature.HealthCenter ? 'Centro de Saúde' :
      value === EntityNature.Hospital ? 'Hospital' :
      value === EntityNature.Office ? 'Loja do Cidadão' :
        'Entidade';
  }

}
