import { LoadingOverlayTaskState } from "./loading-overlay.state";

export interface LoadingOverlayDequeuePayload {
  id: string;
}

export interface LoadingOverlayEnqueuePayload {
  id: string;
}

export interface LoadingOverlayTaskPayload {
  task: LoadingOverlayTaskState;
}
