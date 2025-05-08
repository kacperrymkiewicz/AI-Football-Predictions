import { inject, Injectable, signal } from '@angular/core';
import { Client, MatchPrediction, SportradarHead2HeadResponse, SportradarMatchDetailsResponse, SportradarMatchStatisticsResponse, SportradarResponse } from '../api-client/api-client';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoadingStateService } from './loading-state.service';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  private http = new Client(inject(HttpClient), environment.apiUrl);
  private loadingStateService: LoadingStateService = inject(LoadingStateService);

  liveMatches = signal<SportradarResponse | null>(null);

  public getLiveMatches() {
    this.loadingStateService.setState('liveMatches', true, false);
    return this.http.getLiveMatches().pipe(
      tap({
        next: (matches) => {
          this.liveMatches.set(matches);
        },
        error: (err) => {
          this.loadingStateService.setState('liveMatches', false, true);
          console.error('Błąd pobierania meczów:', err);
        },
        complete: () => {
          this.loadingStateService.setState('liveMatches', false);
        }
      })
    )
  }

  public getMatches(date: Date) {
    this.loadingStateService.setState('liveMatches', true, false);
    const endDate = new Date(date);
    endDate.setDate(date.getDate() + 1);
    return this.http.getMatches(date, endDate).pipe(
      tap({
        next: (matches) => {
          this.liveMatches.set(matches);
        },
        error: (err) => {
          this.loadingStateService.setState('liveMatches', false, true);
          console.error('Błąd pobierania meczów:', err);
        },
        complete: () => {
          this.loadingStateService.setState('liveMatches', false);
        }
      })
    )
  }

  public getMatchDetails(matchId: number) {
    return this.http.getMatchDetails(matchId);
  }

  public getMatchStatistics(matchId: number) {
    return this.http.getMatchStatistics(matchId);
  }

  public getH2hMatches(matchId: number) {
    return this.http.getH2hMatches(matchId);
  }

  public getMatchPredictionById(matchId: number) {
    return this.http.getMatchPredictionById(matchId);
  }
}
