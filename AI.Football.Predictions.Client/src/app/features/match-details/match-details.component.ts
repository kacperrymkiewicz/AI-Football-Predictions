import { Component, inject, OnInit } from '@angular/core';
import { SportradarHead2HeadResponse, SportradarMatchDetailsResponse, SportradarMatchStatisticsResponse } from '../../core/api-client/api-client';
import { MatchService } from '../../core/services/match.service';
import { LoadingStateService } from '../../core/services/loading-state.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-match-details',
  standalone: true,
  imports: [],
  templateUrl: './match-details.component.html',
  styleUrl: './match-details.component.scss'
})
export class MatchDetailsComponent implements OnInit {
  protected matchService: MatchService = inject(MatchService);
  protected loadingStateService: LoadingStateService = inject(LoadingStateService);
  protected route: ActivatedRoute = inject(ActivatedRoute);

  match!: SportradarMatchDetailsResponse;
  matchStatistics!: SportradarMatchStatisticsResponse;
  matchesH2h!: SportradarHead2HeadResponse;

  ngOnInit(): void {
    this.fetchMatchDetails();
    this.fetchMatchStatistics();
    this.fetchH2hMatches();
  }

  fetchMatchDetails() {
    const matchId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadingStateService.setState("matchDetails", true);

    this.matchService.getMatchDetails(matchId).subscribe({
      next: (response) => {
        this.match = response;
      },
      error: () => {
        this.loadingStateService.setState("matchDetails", false, true);
      },
      complete: () => {
        this.loadingStateService.setState("matchDetails", false);
      },
    });
  }

  fetchMatchStatistics() {
    const matchId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadingStateService.setState("matchStatistics", true);

    this.matchService.getMatchStatistics(matchId).subscribe({
      next: (response) => {
        this.matchStatistics = response;
      },
      error: () => {
        this.loadingStateService.setState("matchStatistics", false, true);
      },
      complete: () => {
        this.loadingStateService.setState("matchStatistics", false);
      },
    });
  }

  fetchH2hMatches() {
    const matchId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadingStateService.setState("matchesH2h", true);

    this.matchService.getH2hMatches(matchId).subscribe({
      next: (response) => {
        this.matchesH2h = response;
      },
      error: () => {
        this.loadingStateService.setState("matchesH2h", false, true);
      },
      complete: () => {
        this.loadingStateService.setState("matchesH2h", false);
      },
    });
  }

  formatDate(date: Date) {
    return date.getDate().toString().padStart(2, '0') + '.' + (date.getMonth() + 1).toString().padStart(2, '0') + '.' + 
           date.getFullYear() + ' ' + ("00" + date.getHours()).slice(-2) + ":" + ("00" + date.getMinutes()).slice(-2);  
  }

  get homeStats() {
    return this.matchStatistics?.statistics?.filter(
      s => s.competitorId === this.match?.game?.homeCompetitor?.id
    ) || [];
  }

  get awayStats() {
    return this.matchStatistics?.statistics?.filter(
      s => s.competitorId === this.match?.game?.awayCompetitor?.id
    ) || [];
  }
}
