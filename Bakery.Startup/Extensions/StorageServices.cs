using Bakery.Application.Factories;
using Bakery.Domain.Abstractions;
using Bakery.Infrastructure.Storage.Context;
using Bakery.Infrastructure.Storage.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Startup.Extensions;

///<summary>
/// Статический класс сервисов хранилища.
///</summary>
public static class StorageServices
{
    ///<summary>
    /// Расширяющий метод для добавления сервисов хранилища в коллекцию служб.
    ///</summary>
    ///<param name="services">Коллекция служб.</param>
    ///<param name="configuration">Конфигурация приложения.</param>
    public static void AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Получение строки подключения из конфигурации
        var appConnectionString = configuration.GetRequiredValue<string>("ConnectionStrings:ApplicationDb");
        
        // Добавление контекста базы данных для команд
        services.AddDbContext<ApplicationDbContext>(builder =>
        {
            // Настраиваем использование PostgreSQL с указанной строкой подключения
            builder.UseNpgsql(appConnectionString, opt =>
            {
                // Устанавливаем поведение разделения запросов на SplitQuery
                opt.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });
        });
        
        // Регистрация синглтона для репозитория булочек
        services.AddScoped<IBunRepository, BunRepository>();
        
        // Регистрация синглтона для фабрики булочек
        services.AddSingleton<IBunFactory, RandomBunFactory>();
    }
}