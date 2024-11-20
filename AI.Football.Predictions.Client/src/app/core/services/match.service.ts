import { inject, Injectable, signal } from '@angular/core';
import { Client, Match } from '../api-client/api-client';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  private http = new Client(inject(HttpClient), environment.apiUrl);

  liveMatches = signal<Match[]>([]);
  isFetching = signal<boolean>(true);

  public getLiveMatches(): Observable<Match[]> {
    this.isFetching.set(true);
    return this.http.getLiveMatches().pipe(
      tap({
        next: (matches) => {
          this.liveMatches.set(matches);
        },
        error: (err) => {
          console.error('Błąd pobierania meczów:', err);
        },
        complete: () => {
          this.isFetching.set(false);
        }
      })
    )
  }
}
