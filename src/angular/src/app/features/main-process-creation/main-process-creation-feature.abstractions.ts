export type MainProcessCreationFeatureOptions = {
  step: MainProcessCreationFeatureStep;
};

export enum MainProcessCreationFeatureStep {
  EntitySelection,
  PatientSelection,
  Confirmation
}
