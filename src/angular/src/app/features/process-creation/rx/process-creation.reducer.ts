import { createReducer, on } from "@ngrx/store";
import { init, dispose, configure, step, searchEntitiesComplete, selectEntity } from "./process-creation.actions";
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
    entities: {
      ...state.entities,
      ...payload.entities.reduce(
        (pv, cv) => ({ ...pv, [cv.id]: { ...cv }}),
        {}
      )
    },
    entitySearchResult: {
      entityIds: payload.entityIds,
      totalMatchCount: payload.totalMatchCount
    }
  })),

  on(selectEntity, (state, { payload }) => ({
    ...state,
    entityId: payload.id
  }))
);
