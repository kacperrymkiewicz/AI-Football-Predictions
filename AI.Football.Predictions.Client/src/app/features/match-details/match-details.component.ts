import { Component, inject, OnInit } from '@angular/core';
import { MatchPrediction, MatchScorePrediction, SportradarHead2HeadResponse, SportradarMatchDetailsResponse, SportradarMatchStatisticsResponse } from '../../core/api-client/api-client';
import { MatchService } from '../../core/services/match.service';
import { LoadingStateService } from '../../core/services/loading-state.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { MatchDatePipe } from "../../core/pipes/match-date.pipe";
import { environment } from '../../../environments/environment';
import { MatchCountdownComponent } from "./match-countdown/match-countdown.component";
import { slugify } from '../../core/utils/slugify';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MAT_RIPPLE_GLOBAL_OPTIONS } from '@angular/material/core';
import { TabRecentPerformanceComponent } from "./tab-recent-performance/tab-recent-performance.component";

@Component({
  selector: 'app-match-details',
  standalone: true,
  imports: [MatchDatePipe, MatchCountdownComponent, RouterLink, MatTabsModule, MatButtonModule, TabRecentPerformanceComponent],
  providers: [
    { provide: MAT_RIPPLE_GLOBAL_OPTIONS, useValue: { disabled: true } }
  ],
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
  matchPrediction!: MatchPrediction;
  matchScorePrediction!: MatchScorePrediction;

  activeTab: string = "h2h";

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.fetchMatchDetails();
      this.fetchMatchStatistics();
      this.fetchH2hMatches();
      this.fetchMatchPrediction();
      this.fetchMatchScorePrediction();
    });
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

  fetchMatchPrediction() {
    const matchId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadingStateService.setState("matchPrediction", true);

    this.matchService.getMatchPredictionById(matchId).subscribe({
      next: (response) => {
        this.matchPrediction = response;
      },
      error: () => {
        this.loadingStateService.setState("matchPrediction", false, true);
      },
      complete: () => {
        this.loadingStateService.setState("matchPrediction", false);
      },
    });
  }

  fetchMatchScorePrediction() {
    const matchId = Number(this.route.snapshot.paramMap.get("id"));
    this.loadingStateService.setState("matchScorePrediction", true);

    this.matchService.getMatchScorePredictionById(matchId).subscribe({
      next: (response) => {
        this.matchScorePrediction = response;
      },
      error: () => {
        this.loadingStateService.setState("matchScorePrediction", false, true);
      },
      complete: () => {
        this.loadingStateService.setState("matchScorePrediction", false);
      },
    });
  }

  getTeamLogoUrl(id: number, size: number = 20) {
    return `${environment.imageCacheUrl}/f_png,w_${size},h_${size},c_limit,q_auto:eco,dpr_2,d_Competitors:default1.png/v1/Competitors/${id}`  
  }
  
  getCompetitionLogoUrl(id: number) {
    return `${environment.imageCacheUrl}/f_png,w_10,h_10,c_limit,q_auto:eco,dpr_2,d_Countries:Round:2.png/v5/Competitions/${id}`  
  }

  slugify(text: string): string {
    return slugify(text);
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

  get homeWins(): number {
    const homeTeamId = this.match?.game?.homeCompetitor?.id;
    return this.matchesH2h.game?.h2hGames?.filter(match =>
      (match.homeCompetitor?.id === homeTeamId && match.homeCompetitor?.score! > match.awayCompetitor?.score!) ||
      (match.awayCompetitor?.id === homeTeamId && match.awayCompetitor?.score! > match.homeCompetitor?.score!)
    ).length ?? 0;
  }
  
  get awayWins(): number {
    const awayTeamId = this.match?.game?.awayCompetitor?.id;
    return this.matchesH2h.game?.h2hGames?.filter(match =>
      (match.homeCompetitor?.id === awayTeamId && match.homeCompetitor?.score! > match.awayCompetitor?.score!) ||
      (match.awayCompetitor?.id === awayTeamId && match.awayCompetitor?.score! > match.homeCompetitor?.score!)
    ).length ?? 0;
  }
  
  get draws(): number {
    return this.matchesH2h.game?.h2hGames?.filter(match =>
      (match.homeCompetitor?.id === this.match?.game?.homeCompetitor?.id || 
       match.awayCompetitor?.id === this.match?.game?.homeCompetitor?.id) &&
      match.homeCompetitor?.score === match.awayCompetitor?.score
    ).length ?? 0;
  }

  get mergedStats(): { name?: string, homeValue: any, awayValue: any }[] {
    const categories = new Set([
      ...this.homeStats.map(s => s.name),
      ...this.awayStats.map(s => s.name),
    ].filter((name): name is string => !!name));

    return Array.from(categories).map(name => {
      const home = this.homeStats.find(s => s.name === name);
      const away = this.awayStats.find(s => s.name === name);
      return {
        name,
        homeValue: home?.value ?? '-',
        awayValue: away?.value ?? '-'
      };
    });
  }


  formatAsPercent(value: number): string {
    const percent = value * 100;
    return percent.toFixed(2) + '%';
  }  
}
