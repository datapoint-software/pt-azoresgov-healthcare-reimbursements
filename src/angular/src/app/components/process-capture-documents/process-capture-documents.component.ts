import { Component } from "@angular/core";
import { ProcessCaptureDocumentManager } from "../../features/process-capture/managers/process-capture-document.manager";
import { FormGroupComponent } from "../form-group/form-group.component";
import { ReactiveFormsModule } from "@angular/forms";
import { IntegerPipe } from "../../pipes/integer.pipe";
import { ModalDirective } from "../../directives/modal.directive";
import { CommonModule } from "@angular/common";
import { DocumentNature } from "../../enums/document-nature.enum";
import { DocumentNaturePipe } from "../../pipes/document-nature.pipe";
import { documentNatureOptions } from "../../helpers/document-nature.helper";

@Component({
  imports: [
    CommonModule,
    DocumentNaturePipe,
    FormGroupComponent,
    IntegerPipe,
    ModalDirective,
    ReactiveFormsModule
  ],
  selector: 'app-process-capture-documents',
  standalone: true,
  templateUrl: './process-capture-documents.component.html'
})
export class ProcessCaptureDocumentsComponent {

  public readonly DocumentNature = DocumentNature;

  constructor(
    public readonly documents: ProcessCaptureDocumentManager
  ) {}

  public readonly invoices = this.documents.form.controls.invoices;

  public readonly invoiceControls = this.invoices.controls;

  public readonly otherDocuments = this.documents.form.controls.otherDocuments;

  public readonly otherDocumentControls = this.otherDocuments.controls;

  public readonly otherDocumentNatureOptions = documentNatureOptions({
    exclusions: [ DocumentNature.Invoice ]
  });
}
