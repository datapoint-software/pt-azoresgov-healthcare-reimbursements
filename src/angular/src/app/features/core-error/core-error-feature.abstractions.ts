export type CoreErrorFeatureError = {
  id: string | null;
  correlationId: string | null;
  message: string | null;
  status: CoreErrorFeatureErrorStatus | null;
  stackTrace: string | null;
};

export type CoreErrorFeatureErrorStatus = {
  code: number;
  message: string;
};
