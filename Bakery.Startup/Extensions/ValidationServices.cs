using Bakery.Infrastructure.Web.Buns.Validators;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Bakery.Startup.Extensions;

///<summary>
/// Статический класс сервисов валидации.
///</summary>
public static class ValidationServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов валидации в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    public static void AddValidationServices(this IServiceCollection services)
    {
        // Добавляем все валидаторы в сборке
        services.AddValidatorsFromAssemblyContaining<StartBakingValidator>();
        
        // Добавляем интеграцию валидаторов с валидацией ASP.NET
        services.AddFluentValidationAutoValidation();
    }
}