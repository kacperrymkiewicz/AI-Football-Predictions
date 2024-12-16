import { Component, Input } from '@angular/core';
import { Match } from '../../../core/api-client/api-client';

@Component({
  selector: 'app-match-item',
  standalone: true,
  imports: [],
  templateUrl: './match-item.component.html',
  styleUrl: './match-item.component.scss'
})
export class MatchItemComponent {
  @Input({ required: true }) match!: Match
}
