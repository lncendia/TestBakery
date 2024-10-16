using Bakery.Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Infrastructure.Storage.DatabaseInitialization;

/// <summary>
/// Класс для инициализации начальных данных в базу данных
/// </summary>
public static class ApplicationDatabaseInitializer
{
    /// <summary>
    /// Инициализация начальных данных в базу данных
    /// </summary>
    /// <param name="serviceProvider">Провайдер DI.</param>
    public static async Task InitAsync(IServiceProvider serviceProvider)
    {
        // Получаем контекст базы данных
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        //обновляем базу данных
        await context.Database.MigrateAsync();
    }
}