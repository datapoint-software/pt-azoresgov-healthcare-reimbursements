import { EntityNature } from "@app/enums";

export type MainProcessCaptureFeatureEntityModel = {
  id: string;
  rowVersionId: string;
  parentEntityId?: string;
  code: string;
  name: string;
  nature: EntityNature;
};

export type MainProcessCaptureFeatureLegalRepresentativeModel = {
  id: string;
  rowVersionId: string;
  taxNumber: string;
  name: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  postalAddressArea: string;
  postalAddressAreaCode: string;
  postalAddressLine1: string;
  postalAddressLine2?: string;
  postalAddressLine3?: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeRemoveModel = {
  processId: string;
  processRowVersionId: string;
  patientRowVersionId: string;
  legalRepresentativeRowVersionId: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeRemoveResultModel = {
  processRowVersionId: string;
  patientRowVersionId: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeSelectModel = {
  processId: string;
  processRowVersionId: string;
  patientRowVersionId: string;
  taxNumber: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeSelectResultModel = {
  processRowVersionId: string;
  patientRowVersionId: string;
  legalRepresentative?: MainProcessCaptureFeatureLegalRepresentativeModel;
};

export type MainProcessCaptureFeatureLegalRepresentativeSubmitModel = {
  processId: string;
  processRowVersionId: string;
  patientRowVersionId: string;
  legalRepresentativeId?: string;
  legalRepresentativeRowVersionId?: string;
  taxNumber?: string;
  name: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  postalAddressArea: string;
  postalAddressAreaCode: string;
  postalAddressLine1: string;
  postalAddressLine2?: string;
  postalAddressLine3?: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeSubmitResultModel = {
  processRowVersionId: string;
  patientRowVersionId: string;
  legalRepresentativeId?: string;
  legalRepresentativeRowVersionId: string;
};

export type MainProcessCaptureFeatureOptionsModel = {
  processId: string;
};

export type MainProcessCaptureFeatureOptionsResultModel = {
  entities: MainProcessCaptureFeatureEntityModel[];
  patient: MainProcessCaptureFeaturePatientModel;
  process: MainProcessCaptureFeatureProcessModel;
  legalRepresentative?: MainProcessCaptureFeatureLegalRepresentativeModel;
};

export type MainProcessCaptureFeaturePatientModel = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  taxNumber: string;
  name: string;
  birth: string;
  death?: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  postalAddressArea: string;
  postalAddressAreaCode: string;
  postalAddressLine1: string;
  postalAddressLine2?: string;
  postalAddressLine3?: string;
};

export type MainProcessCaptureFeatureProcessModel = {
  id: string;
  rowVersionId: string;
  entityId: string;
  number: string;
  creation: string;
};

export type MainProcessCaptureFeaturePatientSubmitModel = {
  processId: string;
  processRowVersionId: string;
  patientId: string;
  patientRowVersionId: string;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  postalAddressArea: string;
  postalAddressAreaCode: string;
  postalAddressLine1: string;
  postalAddressLine2?: string;
  postalAddressLine3?: string;
};

export type MainProcessCaptureFeaturePatientSubmitResultModel = {
  processRowVersionId: string;
  patientRowVersionId: string;
};
