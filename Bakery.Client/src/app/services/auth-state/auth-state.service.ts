import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {jwtDecode} from 'jwt-decode';

@Injectable({
  providedIn: 'root'
})

export class AuthStateService {

  /**
   * Ключ для хранения ссылки на аккаунт в localStorage.
   * @returns {string} Ключ для доступа к сохранённому токену.
   */
  public get storedTokenLink(): string {
    return `token`;
  }

  /**
   * Текущий токен пользователя как Observable.
   * Используется для отслеживания изменений в состоянии аутентификации.
   */
  public currentToken$ = new BehaviorSubject<string | null>(null);

  /**
   * Конструктор сервиса AuthStateService.
   * Инициализирует текущую сессию на основе данных, сохранённых в localStorage.
   */
  constructor() {
    const storedToken = localStorage.getItem(this.storedTokenLink);
    this.currentToken$ = new BehaviorSubject<string | null>(storedToken ? JSON.parse(storedToken) : null);
  }

  /**
   * Устанавливает текущий аккаунт и сохраняет его в localStorage.
   * @param token - Токен пользователя, который необходимо установить.
   */
  public setCurrentToken(token: string): void {
    this.currentToken$.next(token);
    localStorage.setItem(this.storedTokenLink, JSON.stringify(token));
  }

  /**
   * Получить текущий токен сессии
   * @returns {string} Access токен.
   */
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

  /**
   * Извлекает username из токена, если он присутствует.
   * @returns {string | null} Имя пользователя или null, если токен не содержит username.
   */
  public getUsernameFromToken(): string | null {
    const token = this.getCurrentToken();
    if (token) {
      try {
        const decodedToken: any = jwtDecode(token);
        return decodedToken?.name || null;
      } catch (error) {
        console.error('Ошибка при декодировании токена', error);
        return null;
      }
    }
    return null;
  }

  /**
   * Выполняет выход пользователя из системы
   */
  public logout(): void {
    localStorage.removeItem(this.storedTokenLink);
    this.currentToken$.next(null);
  }
}
