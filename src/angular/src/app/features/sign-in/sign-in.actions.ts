import { createAction, props } from "@ngrx/store";
import { ErrorModel } from "../../app.models";
import { prefix } from "./sign-in.constants";

export const dispose = createAction(
  `${prefix}/dispose`
);

export const redirect = createAction(
  `${prefix}/redirect`
);

export namespace init {

  const nsprefix = `${prefix}/init`;

  export const begin = createAction(
    nsprefix,
    props<{
      payload: {
        redirectUrl?: string;
      }
    }>()
  );

  export const configure = createAction(
    `${nsprefix}/configure`,
    props<{
      payload: {
        authentication: {
          enabled: boolean;
          persistentEnabled: boolean;
        },
        redirectUrl?: string;
      }
    }>()
  );

  export const getOptions = createAction(
    `${nsprefix}/get-options`,
    props<{ payload: {
      redirectUrl?: string;
    }}>()
  );
};

export namespace submit {

  const nsprefix = `${prefix}/submit`;

  export const begin = createAction(
    nsprefix,
    props<{
      payload: {
        emailAddress: string;
        password: string;
        persistent: boolean;
      }
    }>()
  );

  export const post = createAction(
    `${nsprefix}/post`,
    props<{
      payload: {
        emailAddress: string;
        password: string;
        persistent: boolean;
      }
    }>()
  );

  export const error = createAction(
    `${nsprefix}/error`,
    props<{
      payload: ErrorModel
    }>()
  );
}
