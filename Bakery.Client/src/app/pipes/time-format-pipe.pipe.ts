import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'timeFormatPipe',
  standalone: true
})

export class TimeFormatPipe implements PipeTransform {

  /**
   * Преобразует строку времени в формат "часы:минуты" и форматирует его в виде "X часов Y минут".
   * Учитывает дни, если они присутствуют в строке времени.
   * @param timeString - Строка времени, которая может содержать дни, часы, минуты и миллисекунды.
   * @returns Отформатированное время в виде 'X часов Y минут' или 'Y минут', если часов нет.
   */
  transform(timeString: string): string {
    let parts = timeString.split('.');
    let days = 0;
    let hours = 0;
    let minutes = 0;
    parts.pop();

    /**
     * Парсим строку времени
     */
    if (parts.length > 1) {
      days = Number(parts[0]);
      parts = parts[1].split(':');
      hours = Number(parts[0]);
      minutes = Number(parts[1]);
    } else {
      parts = parts[0].split(':');
      hours = Number(parts[0]);
      minutes = Number(parts[1]);
    }

    const totalHours = days * 24 + hours;

    if (minutes >= 60) {
      const extraHours = Math.floor(minutes / 60);
      minutes %= 60;
      hours += extraHours;
    }

    /**
     * Форматируем часы
     */
    let hoursText = '';
    if (totalHours === 1 || totalHours % 24 === 1) {
      hoursText = `${totalHours} час`;
    } else if ((totalHours === 21) || (totalHours % 24 === 21)) {
      hoursText = `${totalHours} час`;
    } else if ((totalHours > 1 && totalHours < 5) ||
      (totalHours % 24 > 1 && totalHours % 24 < 5)) {
      hoursText = `${totalHours} часа`;
    } else {
      hoursText = `${totalHours} часов`;
    }

    /**
     * Форматируем минуты
     */
    let minutesText = '';
    if (minutes === 1 || minutes === 21) {
      minutesText = `${minutes} минута`;
    } else if (minutes > 1 && minutes < 5 && !(minutes % 10 === 0 || (minutes >= 12 && minutes <= 14))) {
      minutesText = `${minutes} минуты`;
    } else {
      minutesText = `${minutes} минут`;
    }

    if (totalHours === 0) {
      return minutesText;
    }

    return `${hoursText}${minutesText ? ' ' : ''}${minutesText}`.trim() || '0 минут';
  }
}
