using Bakery.Infrastructure.Web.Buns.InputModels;
using FluentValidation;

namespace Bakery.Infrastructure.Web.Buns.Validators;

/// <summary>
/// Валидатор для GetBunsInputModel.
/// </summary>
public class GetBunsValidator : AbstractValidator<GetBunsInputModel>
{
    /// <summary>
    /// Конструктор валидатора
    /// </summary>
    public GetBunsValidator()
    {
        // Правило для Limit
        RuleFor(x => x.Limit)

            // Максимальная длина
            .GreaterThan(0)
            
            // С сообщением
            .WithMessage("Число запрашиваемых булочек не может быть меньше 0")
            
            // Максимальное значение
            .LessThanOrEqualTo(100)

            // С сообщением
            .WithMessage("Максимальное число булочек для получения - 100");
        
        // Правило для Query
        RuleFor(x => x.Offset)
            
            // Максимальная длина
            .GreaterThanOrEqualTo(0)
            
            // С сообщением
            .WithMessage("Смещение не может быть меньше 0");
    }
}