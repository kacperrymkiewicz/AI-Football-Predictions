import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'matchDate',
  standalone: true
})
export class MatchDatePipe implements PipeTransform {
  transform(date: Date | string): string {
    if (!date) return '';
    const d = new Date(date);
    return d.getDate().toString().padStart(2, '0') + '.' +
           (d.getMonth() + 1).toString().padStart(2, '0') + '.' +
           d.getFullYear() + ' ' +
           ("00" + d.getHours()).slice(-2) + ":" +
           ("00" + d.getMinutes()).slice(-2);  
  }
}
