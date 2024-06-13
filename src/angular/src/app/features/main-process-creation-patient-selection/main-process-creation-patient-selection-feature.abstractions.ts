import { FormControl, FormGroup } from "@angular/forms";

export type MainProcessCreationPatientSelectionFeatureForm = FormGroup<{
  filter: FormControl<string | null>;
  full: FormControl<boolean | null>;
}>;

export type MainProcessCreationPatientSelectionFeaturePatient = {
  id: string;
  rowVersionId: string;
  number: string;
  taxNumber: string | null;
  name: string;
  birth: string | null;
  death: string | null;
  external: boolean;
  faxNumber: string | null;
  mobileNumber: string | null;
  phoneNumber: string | null;
  emailAddress: string | null;
  postalAddressArea: string | null;
  postalAddressAreaCode: string | null;
  postalAddressLine1: string | null;
  postalAddressLine2: string | null;
  postalAddressLine3: string | null;
};

export type MainProcessCreationPatientSelectionFeatureSearchResult = {
  totalMatchCount: number;
  patientIds: ReadonlyArray<string>;
};
