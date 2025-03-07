import { Component, Input } from '@angular/core';
import { Game } from '../../../core/api-client/api-client';
import { environment } from '../../../../environments/environment';
import { RouterLink } from '@angular/router';
import { slugify } from '../../../core/utils/slugify';
import { MatchDatePipe } from "../../../core/pipes/match-date.pipe";

@Component({
  selector: 'app-match-item',
  standalone: true,
  imports: [RouterLink, MatchDatePipe],
  templateUrl: './match-item.component.html',
  styleUrl: './match-item.component.scss'
})
export class MatchItemComponent {
  @Input({ required: true }) match!: Game

  getTeamLogoUrl(id: number) {
    return `${environment.imageCacheUrl}/f_png,w_34,h_34,c_limit,q_auto:eco,dpr_2,d_Competitors:default1.png/v1/Competitors/${id}`  
  }

  slugify(text: string): string {
    return slugify(text);
  }
}
