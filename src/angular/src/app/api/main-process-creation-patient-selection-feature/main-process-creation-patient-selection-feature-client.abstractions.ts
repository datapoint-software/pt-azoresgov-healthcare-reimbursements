export type MainProcessCreationPatientSelectionFeatureSearchModel = {
  entityId: string;
  entityRowVersionId: string;
  filter: string;
  mode: MainProcessCreationPatientSelectionFeatureSearchModeModel;
  skip?: number;
  take?: number;
};

export enum MainProcessCreationPatientSelectionFeatureSearchModeModel {
  Full = 70,
  PatientNumber = 78
};

export type MainProcessCreationPatientSelectionFeatureSearchResultModel = {
  totalMatchCount: number;
  patientIds: string[];
  patients: MainProcessCreationPatientSelectionFeatureSearchResultPatientModel[];
};

export type MainProcessCreationPatientSelectionFeatureSearchResultPatientModel = {
  id: string;
  rowVersionId: string;
  number: string;
  taxNumber?: string;
  name: string;
  birth?: string;
  death?: string;
  external: boolean;
  faxNumber?: string;
  mobileNumber?: string;
  phoneNumber?: string;
  emailAddress?: string;
  postalAddressArea?: string;
  postalAddressAreaCode?: string;
  postalAddressLine1?: string;
  postalAddressLine2?: string;
  postalAddressLine3?: string;
};
