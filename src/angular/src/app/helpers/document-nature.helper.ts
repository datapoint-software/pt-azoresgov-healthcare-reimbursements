import { DocumentNature } from "../enums/document-nature.enum"

const DOCUMENT_NATURE_LIST = Object.values(DocumentNature)
  .filter(k => 'number' === typeof(k))
  .map(k => k as unknown as DocumentNature);

const DOCUMENT_NATURE_MESSAGES = {
  [DocumentNature.AddressCertificate]: 'Comprovativo de morada',
  [DocumentNature.DiaperRequisition]: 'Requisição de fraldas',
  [DocumentNature.Generic]: 'Genérico',
  [DocumentNature.IbanCertificate]: 'Comprovativo do IBAN',
  [DocumentNature.IncomeCertificate]: 'Declaração de rendimentos',
  [DocumentNature.Invoice]: 'Fatura/Recibo',
  [DocumentNature.LetterOfAttorney]: 'Procuração',
  [DocumentNature.Permit]: 'Autorização especial',
  [DocumentNature.Prescription]: 'Prescrição clínica',
  [DocumentNature.TreatementCertificate]: 'Guia de tratamento',
  [DocumentNature.UnemploymentCertificate]: 'Declaração de desemprego'
};

export const documentNatureMessage = (documentNature: DocumentNature) =>

  DOCUMENT_NATURE_MESSAGES[documentNature] ||
  DOCUMENT_NATURE_MESSAGES[DocumentNature.Generic];

export const documentNatureOptions = (options?: {
  exclusions?: Array<DocumentNature>
}) =>

  DOCUMENT_NATURE_LIST
    .filter((dn) => !options?.exclusions || 0 > options.exclusions.indexOf(dn))
    .map((dn) => ({ value: dn, label: documentNatureMessage(dn) }))
    .sort((a, b) => a.label.localeCompare(b.label));
