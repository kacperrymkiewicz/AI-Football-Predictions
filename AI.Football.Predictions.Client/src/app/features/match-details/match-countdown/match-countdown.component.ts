import { afterNextRender, Component, effect, Input, OnDestroy, Signal, signal } from '@angular/core';

@Component({
  selector: 'app-match-countdown',
  standalone: true,
  imports: [],
  templateUrl: './match-countdown.component.html',
  styleUrl: './match-countdown.component.scss'
})
export class MatchCountdownComponent {
  @Input() matchDate!: Date;
  countdown = signal(this.calculateTimeLeft());

  constructor() {
    afterNextRender(() => {
      this.updateCountdown();
      setInterval(() => this.updateCountdown(), 1000);
    })
  }

  private calculateTimeLeft(): string {
    if (!this.matchDate) return 'Brak daty meczu';

    const matchTime = this.matchDate.getTime();
    const now = new Date().getTime();
    const diff = matchTime - now;

    if (diff <= 0) return '00:00:00';

    const hours = Math.floor(diff / (1000 * 60 * 60));
    const minutes = Math.floor((diff % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((diff % (1000 * 60)) / 1000);

    return `${this.pad(hours)}:${this.pad(minutes)}:${this.pad(seconds)}`;
  }

  private pad(value: number): string {
    return value.toString().padStart(2, '0');
  }

  private updateCountdown() {
    this.countdown.set(this.calculateTimeLeft());
  }
}