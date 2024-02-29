import { createReducer, on } from "@ngrx/store";
import { init, dispose, configure, step, searchEntitiesComplete } from "./process-creation.actions";
import { ProcessCreationState } from "./process-creation.state";

export const reducer = createReducer(

  (undefined as unknown as ProcessCreationState),

  on(init, () => (undefined as unknown as ProcessCreationState)),
  on(dispose, () => (undefined as unknown as ProcessCreationState)),
  on(configure, (_, { payload }) => ({ ...payload })),

  on(step, (state, { payload }) => ({
    ...state,
    step: payload
  })),

  on(searchEntitiesComplete, (state, { payload }) => ({
    ...state,
    entitySearchResult: {
      ...payload
    }
  }))
);
