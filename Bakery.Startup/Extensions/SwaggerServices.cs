using Microsoft.OpenApi.Models;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Класс для настройки Swagger в приложении.
/// </summary>
public static class SwaggerServices
{
    /// <summary>
    /// Добавляет настройки Swagger в коллекцию служб.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    public static void AddSwaggerServices(this IServiceCollection services)
    {
        // Добавляем Swagger генератор в коллекцию служб
        services.AddSwaggerGen(options =>
        {
            // Настраиваем документацию Swagger
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bakery API", Version = "v1" });

            // Добавляем префикс "api" всем эндпоинтам
            options.DocumentFilter<PathPrefixInsertDocumentFilter>("api");
            
            // Поддержка необязательных ссылочных типов
            options.SupportNonNullableReferenceTypes();

            // Добавляем определение безопасности для Bearer токена
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                // Тип схемы безопасности
                Type = SecuritySchemeType.Http,

                // Формат Bearer токена
                BearerFormat = "JWT",

                // Местоположение параметра
                In = ParameterLocation.Header,

                // Схема
                Scheme = "bearer",

                // Описание
                Description = "Please insert JWT token into field"
            });

            // Добавляем требование безопасности для Bearer токена
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    // Определение схемы безопасности
                    new OpenApiSecurityScheme
                    {
                        // Ссылка на схему безопасности
                        Reference = new OpenApiReference
                        {
                            // Тип ссылки
                            Type = ReferenceType.SecurityScheme,

                            // Идентификатор схемы безопасности
                            Id = "Bearer"
                        }
                    },
                    // Пустой массив для указания, что схема безопасности применяется ко всем операциям
                    Array.Empty<string>()
                }
            });
        });
    }
}