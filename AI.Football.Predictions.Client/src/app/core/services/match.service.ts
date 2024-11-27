import { inject, Injectable, signal } from '@angular/core';
import { Client, Match } from '../api-client/api-client';
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

  liveMatches = signal<Match[]>([]);

  public getLiveMatches(): Observable<Match[]> {
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
}
