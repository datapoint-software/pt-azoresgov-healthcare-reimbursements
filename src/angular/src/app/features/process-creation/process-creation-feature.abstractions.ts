export type ProcessCreationFeatureOptions = {
  step: ProcessCreationFeatureStep;
};

export enum ProcessCreationFeatureStep {
  EntitySelection,
  PatientSelection,
  Confirmation
}
