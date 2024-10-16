import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

// Кастомный валидатор для проверки, что значение положительное
export function positiveNumberValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const value = control.value;
    return value <= 0 ? { 'notPositive': true } : null; // Возвращаем ошибку, если значение 0 или отрицательное
  };
}
