import { ProcessSearchEntityResultModel, ProcessSearchOptionsEntityResultModel, ProcessSearchPatientResultModel, ProcessSearchProcessResultModel } from "../../../clients/process-search/process-search.models";

export interface ProcessSearchState {
  entities: Array<ProcessSearchOptionsEntityResultModel>;
  searchResult?: {
    entities: { [ id: string]: ProcessSearchEntityResultModel; }
    patients: { [ id: string]: ProcessSearchPatientResultModel; };
    processes: Array<ProcessSearchProcessResultModel>;
    totalMatchCount: number;
  };
}
