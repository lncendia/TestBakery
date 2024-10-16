using Serilog;

namespace Bakery.Startup.Extensions;

/// <summary>
/// Статический класс для регистрации Serilog в контейнере DI 
/// </summary>
public static class LoggingServices
{
    /// <summary>
    /// Метод регистрирует Serilog в контейнере DI 
    /// </summary>
    /// <param name="builder">Построитель веб-приложений и сервисов.</param>
    public static void AddLoggingServices(this WebApplicationBuilder builder)
    {
        // Добавляем конфигурацию логгера
        builder.Configuration.AddJsonFile("Configuration/loggers.json");
        
        // Создаем билдер логгера
        Log.Logger = new LoggerConfiguration()
            
            // Считываем значения из конфигурации
            .ReadFrom.Configuration(builder.Configuration)
            
            // Создаем логгер
            .CreateLogger();

        // Используем адаптер логгера Serilog для ASP
        builder.Host.UseSerilog();
    }
}