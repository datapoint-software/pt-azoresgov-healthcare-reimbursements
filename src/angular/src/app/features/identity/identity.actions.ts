import { createAction, props } from "@ngrx/store";

const prefix = '@app/identity';

export const authenticate = createAction(
  `${prefix}/authenticate`,
  props<{
    payload: {
      entities: Array<{
        id: string;
        permissions: Array<{
          id: string;
          name: string;
        }>;
      }>;
      permissions: Array<{
        id: string;
        name: string;
      }>;
      user: {
        id: string;
        name: string;
      };
      userSession: {
        id: string;
      };
    };
  }>()
);

export const dispose = createAction(
  `${prefix}/dispose`
);

export const init = createAction(
  `${prefix}/init`
);

export const initRefresh = createAction(
  `${prefix}/init?refresh`
);

export const initConfigure = createAction(
  `${prefix}/init?configure`,
  props<{
    payload: {
      claims?: {
        entities: Array<{
          id: string;
          permissions: Array<{
            id: string;
            name: string;
          }>;
        }>;
        permissions: Array<{
          id: string;
          name: string;
        }>;
        user: {
          id: string;
          name: string;
        };
        userSession: {
          id: string;
        };
      };
    };
  }>()
);
