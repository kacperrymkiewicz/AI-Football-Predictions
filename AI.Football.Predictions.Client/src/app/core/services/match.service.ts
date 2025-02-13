import { inject, Injectable, signal } from '@angular/core';
import { Client, SportradarHead2HeadResponse, SportradarMatchDetailsResponse, SportradarResponse } from '../api-client/api-client';
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

  public getLiveMatches(): Observable<SportradarResponse> {
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

  public getMatchDetails(matchId: number): Observable<SportradarMatchDetailsResponse> {
    return this.http.getMatchDetails(matchId);
  }

  public getH2hMatches(matchId: number): Observable<SportradarHead2HeadResponse> {
    return this.http.getH2hMatches(matchId);
  }
}
