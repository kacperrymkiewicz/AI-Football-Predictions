import { Component, inject, OnInit } from '@angular/core';
import { SportradarMatchDetailsResponse } from '../../core/api-client/api-client';
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

  ngOnInit(): void {
    this.fetchMatchDetails();
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
}
