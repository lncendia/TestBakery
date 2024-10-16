import {inject} from '@angular/core';
import {AuthStateService} from '../services/auth-state/auth-state.service';
import {Router} from '@angular/router';

/**
 * Функция Guard для защиты маршрутов, требующих аутентификации.
 * Проверяет, авторизован ли пользователь, и перенаправляет его на страницу авторизации, если нет.
 * иначе перенаправляет на страницу авторизации.
 */
export const authGuard = function () {
  /**
   * Получает инстанс сервиса аутентификации.
   */
  const authState = inject(AuthStateService);

  /**
   * Получает инстанс сервиса роутера.
   */
  const router = inject(Router);

  /**
   * Проверяет, авторизован ли пользователь.
   */
  if (authState.isAuth()) {
    return true;
  }

  /**
   * Перенаправляет на страницу авторизации, если пользователь не авторизован.
   */
  return router.parseUrl('/auth');
};
