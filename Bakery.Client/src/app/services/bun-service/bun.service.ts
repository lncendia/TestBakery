import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {AuthStateService} from '../auth-state/auth-state.service';
import {Buns} from '../models/client-models/buns';
import {StartBakingInputModel} from '../models/input-models/start-baking-input-model';
import {GetBunsInputModel} from '../models/input-models/get-buns-input-model';

@Injectable({
  providedIn: 'root'
})

export class BunService {

  /**
   * Текущий залогиненный аккаунт пользователя.
   */
  public token: string;

  /**
   * URL-адрес API для запуска пекарни.
   */
  public get api(): string {
    return `https://localhost:7217`;
  }

  /**
   * Конструктор сервиса BunService.
   * Инициализирует зависимости и получает текущий аккаунт пользователя.
   * @param http - HTTP клиент для отправки запросов.
   * @param authState - Сервис для работы с состоянием аутентификации.
   */
  constructor(private http: HttpClient, private authState: AuthStateService) {
    this.token = this.authState.getCurrentToken();
  }

  /**
   * Метод для запуска пекарни.
   * Отправляет HTTP POST запрос на сервер для запуска процесса выпекания булочек.
   * @returns {Observable<Buns>} Объект Observable, который содержит данные о выпекаемых булочках.
   */
  public startBaking(startBakingInputModel: StartBakingInputModel): Observable<Buns> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });
    const fullApi = this.api + '/api/Bun/StartBaking';

    return this.http.post<Buns>(fullApi, startBakingInputModel, { headers });
  }

  /**
   * Метод для получения булочек.
   * Отправляет HTTP POST запрос на сервер для получения булочек.
   * @returns {Observable<Buns>} Объект Observable, который содержит данные о булочках.
   */
  public getBuns(getBunsInputModel: GetBunsInputModel): Observable<Buns> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`
    });

    const params = new HttpParams()
      .set('Limit', getBunsInputModel.limit.toString())
      .set('Offset', getBunsInputModel.offset?.toString() || '0');

    const fullApi = this.api + '/api/Bun/GetBuns';

    return this.http.get<Buns>(fullApi, { headers, params });
  }
}
