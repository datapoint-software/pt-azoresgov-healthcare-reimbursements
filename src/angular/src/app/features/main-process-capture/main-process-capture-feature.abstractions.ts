import { FormControl, FormGroup } from "@angular/forms";
import { EntityNature, ProcessPaymentMethod, ProcessPaymentRecipient } from "@app/enums";

export type MainProcessCaptureFeatureForm = FormGroup<{
  patient: FormGroup<{
    identity: FormGroup<{
      number: FormControl<string | null>;
      taxNumber: FormControl<string | null>;
      name: FormControl<string | null>;
      birth: FormControl<string | null>;
      death: FormControl<string | null>;
    }>;
    contacts: FormGroup<{
      faxNumber: FormControl<string | null>;
      mobileNumber: FormControl<string | null>;
      phoneNumber: FormControl<string | null>;
      emailAddress: FormControl<string | null>;
    }>;
    postalAddress: FormGroup<{
      postalAddressArea: FormControl<string | null>;
      postalAddressAreaCode: FormControl<string | null>;
      postalAddressLine1: FormControl<string | null>;
      postalAddressLine2: FormControl<string | null>;
      postalAddressLine3: FormControl<string | null>;
    }>;
  }>;
  familyIncomeStatement: FormGroup<{
    year: FormControl<number | null>;
    accessCode: FormControl<string | null>;
    memberCount: FormControl<number | null>;
    amount: FormControl<number | null>;
  }>,
  legalRepresentativeSearch: FormGroup<{
    taxNumber: FormControl<string | null>;
  }>,
  legalRepresentative: FormGroup<{
    identity: FormGroup<{
      taxNumber: FormControl<string | null>;
      name: FormControl<string | null>;
    }>;
    contacts: FormGroup<{
      faxNumber: FormControl<string | null>;
      mobileNumber: FormControl<string | null>;
      phoneNumber: FormControl<string | null>;
      emailAddress: FormControl<string | null>;
    }>;
    postalAddress: FormGroup<{
      postalAddressArea: FormControl<string | null>;
      postalAddressAreaCode: FormControl<string | null>;
      postalAddressLine1: FormControl<string | null>;
      postalAddressLine2: FormControl<string | null>;
      postalAddressLine3: FormControl<string | null>;
    }>;
  }>;
  payment: FormGroup<{
    method: FormControl<ProcessPaymentMethod | null>;
    recipient: FormControl<ProcessPaymentRecipient | null>;
    wireTransferDetails: FormGroup<{
      iban: FormControl<string | null>;
      swiftCode: FormControl<string | null>;
    }>;
  }>;
  unemploymentStatement: FormGroup<{
    issue: FormControl<string | null>;
  }>;
}>;

export type MainProcessCaptureFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntityId: string | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCaptureFeaturePatient = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber: string;
  name: string;
  birth: string;
  death: string | null;
  faxNumber: string | null;
  mobileNumber: string | null;
  phoneNumber: string | null;
  emailAddress: string | null;
  postalAddressArea: string;
  postalAddressAreaCode: string;
  postalAddressLine1: string;
  postalAddressLine2: string | null;
  postalAddressLine3: string | null;
};

export type MainProcessCaptureFeatureProcess = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  creation: string;
};

export enum MainProcessCaptureFeatureStep {
  Confirmation,
  FamilyIncomeStatement,
  LegalRepresentative,
  Patient,
  Payment,
  UnemploymentStatement
};
