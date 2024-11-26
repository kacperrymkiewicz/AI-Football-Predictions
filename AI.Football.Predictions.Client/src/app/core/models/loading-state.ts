export interface LoadingState {
  [key: string]: {
    isFetching: boolean;
    startTime?: Date;
    error?: string;
  };
}
