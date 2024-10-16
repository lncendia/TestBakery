using Bakery.Application.Models;
using Bakery.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Статический класс, представляющий методы для добавления ASP.NET Identity.
/// </summary>
public static class AspIdentity
{
    /// <summary>
    /// Добавляет ASP.NET Identity в коллекцию сервисов.
    /// </summary>
    /// <param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddAspIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        // Получение строки подключения из конфигурации
        var authenticationConnectionString = configuration.GetRequiredValue<string>("ConnectionStrings:AuthenticationDb");
        
        // Добавление контекста базы данных для команд
        services.AddDbContext<AuthenticationDbContext>(builder =>
        {
            // Настраиваем использование PostgreSQL с указанной строкой подключения
            builder.UseNpgsql(authenticationConnectionString, opt =>
            {
                // Устанавливаем поведение разделения запросов на SplitQuery
                opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
        
        // Добавляет и настраивает идентификационную систему для указанных пользователей и типов ролей.
        // Устанавливает опции блокировки учетных записей
        services.AddIdentity<User, Role>(opt =>
            {
                // Разрешает применение механизма блокировки для новых пользователей.
                opt.Lockout.AllowedForNewUsers = true;

                // Задает временной интервал блокировки по умолчанию в 15 минут
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                // Устанавливает максимальное количество неудачных попыток входа перед блокировкой
                opt.Lockout.MaxFailedAccessAttempts = 10;

                // Устанавливаем имена утверждений
                opt.ClaimsIdentity.UserIdClaimType = "sub";
                opt.ClaimsIdentity.UserNameClaimType = "name";
                opt.ClaimsIdentity.RoleClaimType = "role";
                opt.ClaimsIdentity.EmailClaimType = "email";
            })

            //Добавляет реализацию Entity Framework хранилищ сведений об удостоверениях.
            .AddEntityFrameworkStores<AuthenticationDbContext>()

            // Добавляет поставщиков токенов по умолчанию, используемых для
            // создания токенов для сброса паролей, операций изменения
            // электронной почты и номера телефона, а также для создания токенов
            // двухфакторной аутентификации.
            .AddDefaultTokenProviders();
    }
}