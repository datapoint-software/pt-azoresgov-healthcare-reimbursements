export interface LoadingOverlayTaskState {
  id: string;
  enqueued: Date;
}

export interface LoadingOverlayState {
  tasks: { [id: string]: LoadingOverlayTaskState },
  visible: boolean;
}
