import { Pipe, PipeTransform } from "@angular/core";
import { DocumentNature } from "../enums/document-nature.enum";
import { documentNatureMessage } from "../helpers/document-nature.helper";

@Pipe({
  standalone: true,
  name: 'documentNature'
})
export class DocumentNaturePipe implements PipeTransform {
  transform(documentNature: DocumentNature) {
    return documentNatureMessage(documentNature);
  }
}
