export interface ErrorModel {
  code?: string;
  message: string;
  innerErrors?: { [key: string]: ErrorModel[]; };
  innerException?: ExceptionModel;
}

export interface ErrorResponseModel {
  id?: string;
  correlationId?: string;
  source: string;
  error: ErrorModel;
}

export interface ExceptionModel {
  name: string;
  fullName?: string;
  message: string;
  source?: string;
  stackTrace?: string;
  innerException?: ExceptionModel;
}
