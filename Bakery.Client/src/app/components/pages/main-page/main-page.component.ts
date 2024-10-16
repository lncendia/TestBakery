import {Component, OnInit} from '@angular/core';
import {Buns} from '../../../services/models/client-models/buns';
import {
  MatCell,
  MatCellDef,
  MatColumnDef,
  MatHeaderCell,
  MatHeaderCellDef,
  MatHeaderRow,
  MatHeaderRowDef,
  MatRow,
  MatRowDef,
  MatTable
} from '@angular/material/table';
import {CurrencyPipe, DatePipe, NgClass, NgIf} from '@angular/common';
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field';
import {AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatButton} from '@angular/material/button';
import {MatInput} from '@angular/material/input';
import {positiveNumberValidator} from '../../../validators/positive.validator';
import {BunService} from '../../../services/bun-service/bun.service';
import {StartBakingInputModel} from '../../../services/models/input-models/start-baking-input-model';
import {GetBunsInputModel} from '../../../services/models/input-models/get-buns-input-model';
import {MatPaginator, PageEvent} from '@angular/material/paginator';
import {Bun} from '../../../services/models/client-models/bun';

@Component({
  selector: 'app-main-page',
  standalone: true,
  imports: [
    MatTable,
    MatHeaderCell,
    MatCell,
    CurrencyPipe,
    MatHeaderRow,
    MatRow,
    MatColumnDef,
    MatCellDef,
    MatRowDef,
    MatHeaderCellDef,
    MatFormField,
    FormsModule,
    MatButton,
    MatInput,
    MatLabel,
    MatHeaderRowDef,
    MatError,
    NgIf,
    ReactiveFormsModule,
    NgClass,
    MatPaginator,
    DatePipe
  ],
  templateUrl: './main-page.component.html',
  styleUrl: './main-page.component.scss'
})

export class MainPageComponent implements OnInit {

  /**
   * Объект, содержащий данные о булочках и их общее количество.
   */
  public buns?: Bun[]

  /**
   * Форма для ввода данных о булочках.
   */
  public bunsForm: FormGroup;

  /**
   * Страница.
   */
  public page: number = 1;

  /**
   * Всего булочек.
   */
  public totalCount: number = 0

  /**
   * Лимит.
   */
  public limit: number = 5;

  /**
   * Интервал.
   */
  private updateInterval?: ReturnType<typeof setInterval>;

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
   * Конструктор компонента. Инициализирует аккаунт и форму.
   */
  constructor(private bunService: BunService) {
    this.bunsForm = this.initBunsForm();
  }

  /**
   * Метод жизненного цикла компонента, который вызывается после его инициализации.
   * Запрашивает список булочек с сервера с указанными лимитом и смещением,
   * и сохраняет полученные данные в локальной переменной.
   */
  public ngOnInit(): void {
    this.getBuns()
    this.startAutoUpdate();
  }

  /**
   * Обновляет информацию о булочках.
   */
  private startAutoUpdate(): void {
    this.updateInterval = setInterval(() => {
      this.getBuns();
    }, 1000);
  }

  /**
   * Получает список булочек с сервера в соответствии с заданными лимитом и смещением.
   */
  private getBuns(): void {
    const getBunsInputModel: GetBunsInputModel = {
      limit: this.limit,
      offset: (this.page - 1) * this.limit,
    };

    this.bunService.getBuns(getBunsInputModel).subscribe({
      next: (buns: Buns) => {
        this.buns = buns.buns;
        this.totalCount = buns.totalCount
      }
    });
  }

  /**
   * Обрабатывает изменение страницы в компоненте пагинации.
   *
   * @param event - Событие, содержащее информацию о текущей странице и размере страницы.
   */
  public onPaginateChange(event: PageEvent): void {
    this.page = event.pageIndex + 1
    this.limit = event.pageSize
    this.getBuns();
  }

  /**
   * Метод для создания и настройки формы булочек.
   * @returns {FormGroup} Объект формы с полем для ввода количества булочек.
   */
  private initBunsForm(): FormGroup {
    return new FormGroup({
      bunsCount: new FormControl<number | null>(
        null,
        [
          Validators.required,
          positiveNumberValidator()
        ]
      ),
    });
  }

  /**
   * Геттер для доступа к контролу количества булочек.
   * @returns {AbstractControl | null} Контрол для поля количества булочек.
   */
  public get bunsCount(): AbstractControl | null {
    return this.bunsForm.get('bunsCount');
  }

  /**
   * Метод для запуска пекарни и получения новых булочек.
   * Проверяет валидность формы, затем отправляет запрос на запуск пекарни.
   * Полученные булочки добавляются в начало массива уже существующих булочек.
   */
  public startBakery(): void {
    if (this.bunsForm.invalid) return;

    const formValue = this.bunsForm!.value;
    const startBakingInputModel: StartBakingInputModel = {
      count: formValue.bunsCount,
    };

    this.bunService.startBaking(startBakingInputModel).subscribe({
      next: (newBuns: Buns) => {
        this.totalCount = newBuns.totalCount;

        if (this.page != 1) return
        const bunsToShow = newBuns.buns.slice(0, this.limit)
        const oldBuns = this.buns?.slice(0, this.limit - bunsToShow.length) ?? []
        this.buns = [...newBuns.buns, ...oldBuns];
      }
    });
  }

  /**
   * Метод для получения названия булочки на русском языке по её английскому названию.
   * Если перевод не найден, возвращает оригинальное название.
   * @param {string} englishName - Название булочки на английском языке.
   * @returns {string} Название булочки на русском языке.
   */
  public getBunNameInRussian(englishName: string): string {
    return this.bunNamesMap[englishName] || englishName;
  }

  /**
   * Преобразует строку времени в формат "часы:минуты".
   * Учитывает дни, если они присутствуют в строке времени.
   *
   * @param timeString - Строка времени, которая может содержать дни, часы, минуты и миллисекунды.
   *                     Формат: "дни.часы:минуты:секунды.миллисекунды" или "часы:минуты:секунды.миллисекунды".
   * @returns Строку в формате "часы:минуты".
   */
  convertTimeStringToTimeString(timeString: string): string {
    let parts = timeString.split('.');
    let days = 0;
    let hours = 0;
    let minutes = 0;
    let time = 0;
    parts.pop();
    if(parts.length > 1){
      days = Number(parts[0]);
      parts = parts[1].split(':');
      hours = Number(parts[0]);
      minutes = Number(parts[1]);
      time = days * 24 + hours;
      return `${time}:${minutes}`;
    }
    else {
      parts = parts[0].split(':');
      hours = Number(parts[0]);
      minutes = Number(parts[1]);
      return `${hours}:${minutes}`;
    }
  }
}
