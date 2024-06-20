import { Pipe, PipeTransform } from "@angular/core";
import { EntityNature } from "@app/enums";

const ENTITY_NATURE_LABELS = {
  [EntityNature.Administrative]: "Entidade administrativa",
  [EntityNature.HealthCenter]: "Centro de Saúde",
  [EntityNature.Hospital]: "Hospital",
  [EntityNature.Office]: "Gabinete",
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
