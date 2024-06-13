export type ErrorResponseModel = {
  id?: string;
  correlationId?: string;
  code?: string;
  message: string;
  innerErrors?: { [key: string]: ErrorModel[]; };
  exception?: ExceptionModel;
};

export type ErrorModel = {
  code?: string;
  message: string;
};

export type ExceptionModel = {
  name: string;
  fullName?: string;
  message: string;
  source?: string;
  stackTrace?: string;
  innerException?: ExceptionModel;
};
