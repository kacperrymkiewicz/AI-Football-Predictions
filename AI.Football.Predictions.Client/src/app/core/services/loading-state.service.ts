import { Injectable, signal } from '@angular/core';
import { LoadingState } from '../models/loading-state';

@Injectable({
  providedIn: 'root'
})
export class LoadingStateService {
  private loadingStates = signal<Record<string, LoadingState>>({});

  setState(key: string, loading: boolean, error: boolean = false): void {
    this.loadingStates.update((currentState) => ({
      ...currentState,
      [key]: { loading, error },
    }));
  }

  getState(key: string): LoadingState {
    return this.loadingStates()[key] || { loading: false, error: false } ;
  }

  isLoading(key: string): boolean {
    return this.getState(key).loading;
  }

  isAnyLoading(): boolean {
    return Object.values(this.loadingStates()).some(state => state.loading);
  }

  hasError(key: string): boolean {
    return this.getState(key).error;
  }

  hasAnyErrors(): boolean {
    return Object.values(this.loadingStates()).some(state => state.error);
  }
}
