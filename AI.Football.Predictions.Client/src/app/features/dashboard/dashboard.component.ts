import { Component, inject } from '@angular/core';
import { MatchService } from '../../core/services/match.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  protected matchService: MatchService = inject(MatchService);

  constructor() {
    this.matchService.getLiveMatches().subscribe();
  }
}
