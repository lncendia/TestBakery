import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'bunName',
  standalone: true
})
export class BunNamePipe implements PipeTransform {

  /**
   * Переменная для маппинга названий булочек.
   */
  private bunNamesMap: { [key: string]: string } = {
    'Baguette': 'Багет',
    'Croissant': 'Круассан',
    'Loaf': 'Батон',
    'Pretzel': 'Крендель',
    'Smetannik': 'Сметанник',
  };

  /**
   * Метод для получения названия булочки на русском языке по её английскому названию.
   * Если перевод не найден, возвращает оригинальное название.
   * @param {string} englishName - Название булочки на английском языке.
   * @returns {string} Название булочки на русском языке.
   */
  transform(englishName: string): string {
    return this.bunNamesMap[englishName] || englishName;
  }
}
