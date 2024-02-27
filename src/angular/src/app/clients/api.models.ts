export interface ErrorResponseModel {
  id: string | null;
  correlationId: string | null;
  code: string | null;
  message: string;
  innerErrors: { [key: string]: ErrorModel[]; } | null;
  exception: ExceptionModel | null;
};

export interface ErrorModel {
  code: string | null;
  message: string;
};

export interface ExceptionModel {
  name: string;
  fullName: string | null;
  message: string;
  source: string | null;
  stackTrace: string | null;
  innerException: ExceptionModel | null;
};
