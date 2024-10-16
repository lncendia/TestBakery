using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Storage.Context;

/// <summary> 
/// Класс контекста базы данных приложения. 
/// </summary> 
/// <param name="options">Параметры контекста базы данных.</param> 
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{ 
    /// <summary> 
    /// Настраивает модель базы данных при создании контекста. 
    /// </summary> 
    /// <param name="modelBuilder">Построитель модели базы данных.</param> 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Применение всех конфигураций в сборке
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        // Вызов базовой реализации метода OnModelCreating для применения настроек модели. 
        base.OnModelCreating(modelBuilder); 
    } 
}