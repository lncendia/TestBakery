import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthStateService {

  /**
   * Ключ для хранения ссылки на аккаунт в localStorage.
   * @returns {string} Ключ для доступа к сохранённому аккаунту.
   */
  get storedTokenLink(): string {
    return `token`;
  }

  /**
   * Текущий аккаунт пользователя как Observable.
   * Используется для отслеживания изменений в состоянии аутентификации.
   */
  public currentToken$ = new BehaviorSubject<string | null>(null);

  /**
   * Конструктор сервиса AuthStateService.
   * Инициализирует текущий аккаунт на основе данных, сохранённых в localStorage.
   */
  constructor() {
    const storedToken = localStorage.getItem(this.storedTokenLink);
    this.currentToken$ = new BehaviorSubject<string | null>(storedToken ? JSON.parse(storedToken) : null);
  }

  /**
   * Устанавливает текущий аккаунт и сохраняет его в localStorage.
   * @param token - Аккаунт пользователя, который необходимо установить.
   */
  public setCurrentToken(token: string): void {
    this.currentToken$.next(token);
    localStorage.setItem(this.storedTokenLink, JSON.stringify(token));
  }

  public getCurrentToken(): string {
    const storedToken = localStorage.getItem(this.storedTokenLink);
    return storedToken ? JSON.parse(storedToken) : null;
  }

  /**
   * Возвращает флаг авторизован ли пользователь.
   * @returns {boolean} Флаг, отвечающий за то авторизован ли пользователь.
   */
  public isAuth(): boolean {
    return !!this.currentToken$.value;
  }
}
