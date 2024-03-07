import { createReducer, on } from "@ngrx/store";
import { ProcessSearchState } from "./process-search.state";
import { configure, dispose, init, searchComplete } from "./process-search.actions";

export const reducer = createReducer(

  (undefined as unknown as ProcessSearchState),

  on(dispose, () => (undefined as unknown as ProcessSearchState)),
  on(init, () => (undefined as unknown as ProcessSearchState)),

  on(configure, (_, { payload }) => ({
    ...payload
  })),

  on(searchComplete, (state, { payload }) => ({
    ...state,
    searchResult: {
      entities: payload.entities.reduce((pv, cv) => ({ ...pv, [cv.id]: { ...cv }}), {}),
      patients: payload.patients.reduce((pv, cv) => ({ ...pv, [cv.id]: { ...cv }}), {}),
      processes: [ ...payload.processes.map(x => ({ ...x })) ],
      totalMatchCount: payload.totalMatchCount
    }
  }))

);
