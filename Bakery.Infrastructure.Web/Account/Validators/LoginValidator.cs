using Bakery.Infrastructure.Web.Account.InputModels;
using FluentValidation;

namespace Bakery.Infrastructure.Web.Account.Validators;

/// <summary>
/// Валидатор для LoginInputModel.
/// </summary>
public class LoginValidator : AbstractValidator<LoginInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public LoginValidator()
    {
        // Правило для Email
        RuleFor(x => x.Email)

            // Не пустое
            .NotEmpty()
            
            // С сообщением
            .WithMessage("Почта обязательна для заполнения")
            
            // Email адрес
            .EmailAddress()

            // С сообщением
            .WithMessage("Формат почты некорректен");
        
        // Правило для Password
        RuleFor(x => x.Password)

            // Не пустое
            .NotEmpty()

            // С сообщением
            .WithMessage("Пароль обязателен для заполнения");
    }
}