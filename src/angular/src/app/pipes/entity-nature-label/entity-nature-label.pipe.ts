import { Pipe, PipeTransform } from "@angular/core";
import { EntityNature } from "@app/enums";

const ENTITY_NATURE_LABELS = {
  [EntityNature.Administrative]: "Unidade de Saúde de Ilha",
  [EntityNature.HealthCenter]: "Centro de Saúde",
  [EntityNature.Hospital]: "Hospital",
  [EntityNature.Office]: "Loja do Cidadão",
};

@Pipe({
  name: "entityNatureLabel",
  standalone: true
})
export class EntityNatureLabelPipe implements PipeTransform {

  transform(value: EntityNature) {
    return ENTITY_NATURE_LABELS[value] ?? "Entidade";
  }
}
