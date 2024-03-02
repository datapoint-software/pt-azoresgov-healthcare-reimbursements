import { ProcessCreationEntityResultModel, ProcessCreationPatientResultModel } from "../../../clients/process-creation/process-creation.models";

export interface ProcessCreationState {

  // Entity selection
  entityId?: string;
  entities: { [id: string]: ProcessCreationEntityResultModel; };
  entitySearchResult?: {
    entityIds: Array<string>;
    totalMatchCount: number;
  };

  // Patient selection
  patientId?: string;
  patients: { [ id: string]: ProcessCreationPatientResultModel; };
  patientSearchResult?: {
    patientIds: Array<string>;
    totalMatchCount: number;
  };

  // Wizard
  step: number;
  steps: Array<string>;
}
