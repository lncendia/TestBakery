import {Component, OnDestroy} from '@angular/core';
import {AbstractControl, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {AuthStateService} from '../../../services/auth-state/auth-state.service';
import {AuthService} from '../../../services/auth-service/auth.service';
import {Subject} from 'rxjs';
import {CommonModule} from '@angular/common';
import {MatError, MatFormField, MatLabel} from '@angular/material/form-field';
import {MatCheckbox} from '@angular/material/checkbox';
import {MatButton} from '@angular/material/button';
import {MatInput} from '@angular/material/input';
import {AccountInputModel} from '../../../services/models/input-models/account-input-model';

@Component({
  selector: 'app-auth-page',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormField,
    MatError,
    MatCheckbox,
    MatButton,
    MatInput,
    MatLabel
  ],
  templateUrl: './auth-page.component.html',
  styleUrl: './auth-page.component.scss'
})

export class AuthPageComponent implements OnDestroy {
  /**
   * Форма для ввода логина и пароля.
   */
  public loginForm: FormGroup;

  /**
   * Subject для управления подписками и их отмены при уничтожении компонента.
   */
  private unsubscribe$ = new Subject<void>();

  /**
   * Конструктор, который инициализирует форму и принимает зависимости.
   * @param router - Сервис для навигации.
   * @param authService - Сервис аутентификации для выполнения запросов.
   * @param authState - Состояние аутентификации для управления текущим аккаунтом.
   */
  constructor(
    private router: Router,
    private authService: AuthService,
    private authState: AuthStateService
  ) {
    /**
     * Инициализирует форму логина.
     */
    this.loginForm = this.initLoginForm();
  }

  /**
   * Метод для создания и настройки формы логина.
   * @returns {FormGroup} Объект формы с полями для логина, пароля и запоминания.
   */
  private initLoginForm(): FormGroup {
    return new FormGroup({
      /**
       * Контрол для логина с валидацией на обязательное заполнение.
       */
      email: new FormControl<string | null>("", [Validators.required, Validators.email]),

      /**
       * // Контрол для пароля с валидацией на обязательное заполнение.
       */
      password: new FormControl<string | null>("", [Validators.required]),

      /**
       * Поле для запоминания (по умолчанию - false).
       */
      remember: new FormControl<boolean | null>(false)
    });
  }

  /**
   * Метод для обработки отправки формы логина.
   * Выполняет вход в систему, если форма валидна.
   */
  public submitLoginForm(): void {
    if (this.loginForm.valid) {
      const formValue = this.loginForm.value;
      const account: AccountInputModel = {
        email: formValue.email,
        password: formValue.password
      };

      this.authService.login(account).subscribe({
        next: (token: string) => {
          this.authState.setCurrentToken(token);
          this.router.navigate(['/main']);
        },
        error: () => {
          this.loginForm.setErrors({ unauthorized: true });
          this.email?.setErrors({ unauthorized: true });
          this.password?.setErrors({ unauthorized: true });
        }
      });
    }
  }

  /**
   * Геттер для доступа к контролу email.
   * @returns {AbstractControl | null} Контрол для поля логина.
   */
  public get email(): AbstractControl | null {
    return this.loginForm.get('email');
  }

  /**
   * Геттер для доступа к контролу пароля.
   * @returns {AbstractControl | null} Контрол для поля пароля.
   */
  public get password(): AbstractControl | null {
    return this.loginForm.get('password');
  }

  /**
   * Геттер для доступа к контролу запоминания.
   * @returns {AbstractControl | null} Контрол для поля запоминания.
   */
  public get remember(): AbstractControl | null {
    return this.loginForm.get('remember');
  }

  /**
   * Метод для обработки фокуса на поле формы.
   * Убирает все ошибки валидации формы.
   */
  public resetFormErrors(): void {
    Object.keys(this.loginForm.controls).forEach((controlName) => {
      this.loginForm.get(controlName)?.setErrors(null);
    });
  }

  /**
   * Метод, вызываемый при уничтожении компонента для очистки ресурсов.
   * Завершает все активные подписки для предотвращения утечек памяти.
   */
  public ngOnDestroy(): void {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
}
