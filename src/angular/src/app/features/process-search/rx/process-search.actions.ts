import { createAction, props } from "@ngrx/store";
import { FEATURE_ACTION_PREFIX } from "../process-search.constants";
import { ProcessSearchState } from "./process-search.state";
import { ProcessSearchResultModel } from "../../../clients/process-search/process-search.models";

export const configure = createAction(
  `${FEATURE_ACTION_PREFIX}/configure`,
  props<{
    payload: ProcessSearchState;
  }>()
);

export const dispose = createAction(
  `${FEATURE_ACTION_PREFIX}/dispose`
);

export const init = createAction(
  `${FEATURE_ACTION_PREFIX}/init`
);

export const search = createAction(
  `${FEATURE_ACTION_PREFIX}/search`,
  props<{
    payload: {
      entityId?: string;
      filter?: string;
      skip?: number;
      take?: number;
    }
  }>()
);

export const searchComplete = createAction(
  `${FEATURE_ACTION_PREFIX}/search-complete`,
  props<{
    payload: ProcessSearchResultModel;
  }>()
);
