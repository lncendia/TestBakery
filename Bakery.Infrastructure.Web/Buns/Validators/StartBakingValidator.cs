using Bakery.Infrastructure.Web.Buns.InputModels;
using FluentValidation;

namespace Bakery.Infrastructure.Web.Buns.Validators;

/// <summary>
/// Валидатор для StartBakingInputModel.
/// </summary>
public class StartBakingValidator : AbstractValidator<StartBakingInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public StartBakingValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Count)

            // Максимальная длина
            .GreaterThan(0)

            // С сообщением
            .WithMessage("Число булочек к изготовлению не может быть меньше 0")

            // Максимальное значение
            .LessThanOrEqualTo(10)

            // С сообщением
            .WithMessage("Максимальное значение булочек к изготовлению - 10");
    }
}