import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'matchDate',
  standalone: true
})
export class MatchDatePipe implements PipeTransform {
  transform(date: Date | string, format: 'date' | 'time' | 'dateTime' | 'fullDate' | 'fullDateTime' = 'dateTime'): string {
    if (!date) return '';
    const d = new Date(date);

    const dateStr = d.getDate().toString().padStart(2, '0') + '.' +
                    (d.getMonth() + 1).toString().padStart(2, '0') + '.' +
                    d.getFullYear();

    const timeStr = d.getHours().toString().padStart(2, '0') + ":" +
                    d.getMinutes().toString().padStart(2, '0');

    if (format === 'date') return dateStr;
    if (format === 'time') return timeStr;
    if (format === 'fullDate') return d.toLocaleDateString('pl-PL', { day: 'numeric', month: 'long', year: 'numeric' });
    if (format === 'fullDateTime') return d.toLocaleDateString('pl-PL', { weekday: 'short', day: 'numeric', month: 'long', year: 'numeric' }) + ', ' + d.toLocaleTimeString('pl-PL', { hour: '2-digit', minute: '2-digit' });
    return `${dateStr} ${timeStr}`;
  }
}