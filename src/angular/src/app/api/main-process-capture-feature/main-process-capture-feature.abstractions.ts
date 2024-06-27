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

export type MainProcessCaptureFeatureLegalRepresentativeSearchModel = {
  taxNumber: string;
};

export type MainProcessCaptureFeatureLegalRepresentativeSearchResultModel = {
  legalRepresentative?: MainProcessCaptureFeatureLegalRepresentativeModel;
};

export type MainProcessCaptureFeatureOptionsModel = {
  processId: string;
};

export type MainProcessCaptureFeatureOptionsResultModel = {
  entities: MainProcessCaptureFeatureEntityModel[];
  patient: MainProcessCaptureFeaturePatientModel;
  process: MainProcessCaptureFeatureProcessModel;
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

export type MainProcessCaptureFeatureSubmitPatientModel = {
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

export type MainProcessCaptureFeatureSubmitPatientResultModel = {
  processRowVersionId: string;
  patientRowVersionId: string;
};
