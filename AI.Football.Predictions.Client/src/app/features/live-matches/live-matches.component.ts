import { Component, inject } from '@angular/core';
import { LoadingStateService } from '../../core/services/loading-state.service';
import { MatchService } from '../../core/services/match.service';
import { MatchItemComponent } from './match-item/match-item.component';

@Component({
  selector: 'app-live-matches',
  standalone: true,
  imports: [MatchItemComponent],
  templateUrl: './live-matches.component.html',
  styleUrl: './live-matches.component.scss'
})
export class LiveMatchesComponent {
  protected matchService: MatchService = inject(MatchService);
  protected loadingStateService: LoadingStateService = inject(LoadingStateService);

  constructor() {
    this.matchService.getLiveMatches().subscribe();
  }
}
