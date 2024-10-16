using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Класс для настройки аутентификации JWT в приложении.
/// </summary>
public static class JwtAuthentication
{
    /// <summary>
    /// Добавляет настройки аутентификации JWT в коллекцию служб.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    /// <param name="configuration">Конфигурация приложения.</param>
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Получаем значение издателя из конфигурации
        var issuer = configuration.GetRequiredValue<string>("Authentication:Issuer");

        // Получаем значение аудитории из конфигурации
        var audience = configuration.GetRequiredValue<string>("Authentication:Audience");

        // Получаем секретный ключ из конфигурации
        var secret = configuration.GetRequiredValue<string>("Authentication:Secret");

        // Добавляем аутентификацию JWT в коллекцию служб
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                // Отключаем требование HTTPS для метаданных
                options.RequireHttpsMetadata = false;

                // Настраиваем параметры валидации токена
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Проверяем издателя токена
                    ValidateIssuer = true,

                    // Устанавливаем допустимого издателя
                    ValidIssuer = issuer,

                    // Проверяем аудиторию токена
                    ValidateAudience = true,

                    // Устанавливаем допустимую аудиторию
                    ValidAudience = audience,

                    // Проверяем срок действия токена
                    ValidateLifetime = true,

                    // Устанавливаем ключ для подписи токена
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

                    // Проверяем ключ подписи издателя
                    ValidateIssuerSigningKey = true
                };
            });

        // Настраиваем схему аутентификации по умолчанию
        services.Configure<AuthenticationOptions>(options =>
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);
    }
}