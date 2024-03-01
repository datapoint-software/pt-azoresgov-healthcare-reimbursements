import { ProcessCreationEntitySearchResultModel } from "../../../clients/process-creation/process-creation.models";

export interface ProcessCreationState {
  step: number;
  steps: Array<string>;
  entity?: { id: string; rowVersionId: string; };
  entitySearchResult?: ProcessCreationEntitySearchResultModel;
}
