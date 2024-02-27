import { STATUS_CODE_DEFAULT_MESSAGE, STATUS_CODE_MESSAGES } from "../error.constants";

export const statusObjectFromCode = (statusCode?: number) => {

  if (!statusCode)
    return undefined;

  return {
    code: statusCode,
    message: STATUS_CODE_MESSAGES[statusCode] || STATUS_CODE_DEFAULT_MESSAGE
  }
};

export const decodeBase64URIComponent = (component?: string) => {

  if (!component)
    return undefined;

  return decodeURIComponent(
    atob(component)
      .replace(/\+/g, '%20'));
};
