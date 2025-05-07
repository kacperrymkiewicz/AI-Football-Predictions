import { Component } from '@angular/core';
import { LiveMatchesComponent } from '../live-matches/live-matches.component';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [LiveMatchesComponent],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {

}
