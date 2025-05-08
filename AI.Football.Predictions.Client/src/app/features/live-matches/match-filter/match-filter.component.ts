import {ChangeDetectionStrategy, Component, inject} from '@angular/core';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {DateAdapter, MAT_DATE_LOCALE, provideNativeDateAdapter} from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { MatchDatePipe } from "../../../core/pipes/match-date.pipe";
import { CustomDateAdapter } from './custom-date-adapter';
import { RelativeDatePipe } from "../../../core/pipes/relative-date.pipe";
import { MatchService } from '../../../core/services/match.service';

@Component({
  selector: 'app-match-filter',
  standalone: true,
  providers: [
    provideNativeDateAdapter(),
    { provide: MAT_DATE_LOCALE, useValue: 'pl-PL' },
    { provide: DateAdapter, useClass: CustomDateAdapter },
  ],
  imports: [MatFormFieldModule, MatInputModule, MatDatepickerModule, FormsModule, RelativeDatePipe],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './match-filter.component.html',
  styleUrl: './match-filter.component.scss'
})
export class MatchFilterComponent {
  protected matchService: MatchService = inject(MatchService);
  selectedDate: Date = new Date();

  changeDate(direction: string): void {
    const newDate = new Date(this.selectedDate);

    if (direction === 'prev') {
      newDate.setDate(newDate.getDate() - 1);
    } else if (direction === 'next') {
      newDate.setDate(newDate.getDate() + 1);
    }
    this.selectedDate = newDate;
    this.onDateChange();
  }

  onDateChange() {
    const correctedDate = new Date(this.selectedDate);
    correctedDate.setHours(12, 0, 0, 0);
    this.matchService.getMatches(correctedDate).subscribe();
  }
}
