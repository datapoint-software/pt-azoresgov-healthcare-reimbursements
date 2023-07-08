export interface ErrorState {
  id?: string;
  message?: string;
  status?: {
    code: number;
    message: string;
  }
}
