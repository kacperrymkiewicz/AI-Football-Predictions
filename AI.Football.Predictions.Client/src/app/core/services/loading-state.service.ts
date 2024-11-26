import { Injectable, signal } from '@angular/core';
import { LoadingState } from '../models/loading-state';

@Injectable({
  providedIn: 'root'
})
export class LoadingStateService {
  private loadingStates = signal<LoadingState>({});

  setLoadingState(key: string, isLoading: boolean): void {
    this.loadingStates.update((state) => {
      const updatedState = { ...state };
      updatedState[key] = { isFetching: isLoading };
      return updatedState;
    });
  }

  isLoading(key: string): boolean {
    return this.loadingStates().hasOwnProperty(key) ? this.loadingStates()[key].isFetching : false;
  }

  isAnyLoading(): boolean {
    return Object.values(this.loadingStates()).some(state => state.isFetching);
  }
}