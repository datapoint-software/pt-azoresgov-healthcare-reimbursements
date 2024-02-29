import { ProcessCreationEntitySearchResultModel } from "../../../clients/process-creation/process-creation.models";

export interface ProcessCreationState {
  step: number;
  steps: Array<string>;
  entitySearchResult?: ProcessCreationEntitySearchResultModel;
}
