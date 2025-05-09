import { Component, inject, Input } from '@angular/core';
import { LoadingStateService } from '../../../core/services/loading-state.service';
import { RecentGame } from '../../../core/api-client/api-client';
import { slugify } from '../../../core/utils/slugify';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-tab-recent-performance',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './tab-recent-performance.component.html',
  styleUrl: './tab-recent-performance.component.scss'
})
export class TabRecentPerformanceComponent {
  @Input() homeCompetitorId!: number;
  @Input() homePerformance!: RecentGame[];
  @Input() awayCompetitorId!: number;
  @Input() awayPerformance!: RecentGame[];

  protected loadingStateService: LoadingStateService = inject(LoadingStateService);

  getResultClass(match: RecentGame, competitorId: number): string {
    const isHome = match.homeCompetitor?.id === competitorId;
    const myScore = isHome ? match.homeCompetitor?.score : match.awayCompetitor?.score;
    const opponentScore = isHome ? match.awayCompetitor?.score : match.homeCompetitor?.score;

    if (myScore == null || opponentScore == null) return 'result-neutral';

    if (myScore > opponentScore) return 'result-win';
    if (myScore < opponentScore) return 'result-loss';
    return 'result-draw';
  }

  slugify(text: string): string {
    return slugify(text);
  }
}
