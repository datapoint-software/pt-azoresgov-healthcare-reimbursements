export interface ErrorState {
  id?: string;
  correlationId?: string;
  message?: string;
  stackTrace?: string;
  status?: {
    code: number;
    message: string;
  };
};
