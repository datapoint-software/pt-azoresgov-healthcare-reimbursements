import { EntityNature } from "@app/enums";

export type MainProcessCreationConfirmationFeatureOptions = {
  entity: MainProcessCreationConfirmationFeatureEntity;
  patient: MainProcessCreationConfirmationFeaturePatient;
};

export type MainProcessCreationConfirmationFeatureEntity = {
  id: string;
  rowVersionId: string;
  parentEntity: MainProcessCreationConfirmationFeatureEntity | null;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCreationConfirmationFeaturePatient = {
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
