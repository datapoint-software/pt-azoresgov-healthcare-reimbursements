export interface ErrorResponseModel {
  id?: string;
  correlationId?: string;
  code?: string;
  message: string;
  innerErrors?: { [key: string]: ErrorModel[]; };
  exception?: ExceptionModel;
};

export interface ErrorModel {
  code?: string;
  message: string;
};

export interface ExceptionModel {
  name: string;
  fullName?: string;
  message: string;
  source?: string;
  stackTrace?: string;
  innerException?: ExceptionModel;
};
