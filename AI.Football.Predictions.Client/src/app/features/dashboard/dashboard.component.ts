import { Component, inject, signal } from '@angular/core';
import { MatchService } from '../../core/services/match.service';
import { LoadingState } from '../../core/models/loading-state';
import { LoadingStateService } from '../../core/services/loading-state.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  protected matchService: MatchService = inject(MatchService);
  protected loadingStateService: LoadingStateService = inject(LoadingStateService);

  constructor() {
    this.matchService.getLiveMatches().subscribe();
  }
}
