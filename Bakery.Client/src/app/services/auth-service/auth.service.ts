import { Injectable } from '@angular/core';
import {Observable} from 'rxjs';
import {AccountInputModel} from '../models/input-models/account-input-model';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class AuthService {


  /**
   * Конструктор сервиса AuthService.
   * @param http - Сервис HttpClient для выполнения HTTP-запросов.
   */
  constructor(private http: HttpClient) { }

  /**
   * Выполняет запрос на вход в систему с использованием данных пользователя.
   * Возвращает Observable с объектом типа string.
   * @param account - Данные пользователя для входа (логин и пароль).
   * @returns {Observable<string>} Observable с объектом string, содержащим access токен.
   */
  public login(account: AccountInputModel): Observable<string> {
    return this.http.post<string>("/api/Account/Token", account, {
      responseType: 'text' as 'json'
    });
  }
}
