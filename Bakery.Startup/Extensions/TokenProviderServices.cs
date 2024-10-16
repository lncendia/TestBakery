using Bakery.Application.TokenProvider;
using Bakery.Infrastructure.Identity.TokenProvider;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Класс для регистрации сервиса TokenProvider в коллекции служб.
/// </summary>
public static class TokenProviderServices
{
    /// <summary>
    /// Добавляет сервис TokenProvider в коллекцию служб.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddTokenProvider(this IServiceCollection services, IConfiguration configuration)
    {
        // Получаем значение издателя из конфигурации
        var issuer = configuration.GetRequiredValue<string>("Authentication:Issuer");

        // Получаем значение аудитории из конфигурации
        var audience = configuration.GetRequiredValue<string>("Authentication:Audience");

        // Получаем секретный ключ из конфигурации
        var secret = configuration.GetRequiredValue<string>("Authentication:Secret");

        // Получаем время жизни токена доступа из конфигурации
        var accessTokenLifetime = configuration.GetRequiredValue<long>("Authentication:AccessTokenLifetime");

        // Регистрируем сервис TokenProvider в коллекции служб
        services.AddSingleton<ITokenProvider, TokenProvider>(_ =>
            new TokenProvider(issuer, audience, secret, TimeSpan.FromMilliseconds(accessTokenLifetime)));
    }
}