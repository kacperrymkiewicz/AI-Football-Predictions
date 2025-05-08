import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'relativeDate',
  standalone: true
})
export class RelativeDatePipe implements PipeTransform {
  transform(date: Date | string): string {
    if (!date) return '';

    const d = new Date(date);

    const today = new Date();
    today.setHours(0, 0, 0, 0);

    const yesterday = new Date(today);
    yesterday.setDate(today.getDate() - 1);

    const tomorrow = new Date(today);
    tomorrow.setDate(today.getDate() + 1);

    if (d.setHours(0, 0, 0, 0) === today.getTime()) {
      return 'Dzisiaj';
    } else if (d.setHours(0, 0, 0, 0) === yesterday.getTime()) {
      return 'Wczoraj';
    } else if (d.setHours(0, 0, 0, 0) === tomorrow.getTime()) {
      return 'Jutro';
    }
    return d.toLocaleDateString('pl-PL', { weekday: 'long', day: 'numeric', month: 'long', year: 'numeric' }).replace(/^./, c => c.toUpperCase());
  }
}
