using Bakery.Infrastructure.Identity.DatabaseInitialization;
using Bakery.Infrastructure.Storage.DatabaseInitialization;
using Bakery.Startup.Extensions;
using Bakery.Startup.Middlewares;
using Newtonsoft.Json;

// Создание экземпляра объекта builder с использованием переданных аргументов
var builder = WebApplication.CreateBuilder(args);

// Добавляем файл appsettings.json в конфигурацию
builder.Configuration.AddJsonFile("Configuration/appsettings.json");

// Регистрируем сервисы логгирования
builder.AddLoggingServices();

// Регистрация Swagger генератора
builder.Services.AddSwaggerServices();

// Добавляем сервисы валидации
builder.Services.AddValidationServices();

// Добавление служб Mediator
builder.Services.AddMediatorServices();

// Добавляем сервисы ASP.NET Identity
builder.Services.AddAspIdentity(builder.Configuration);

// Добавляем сервисы аутентификации JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// Добавляем сервисы для предоставления токенов доступа
builder.Services.AddTokenProvider(builder.Configuration);

// Добавление служб для хранилища приложения
builder.Services.AddStorageServices(builder.Configuration);

// Добавление служб для маппинга
builder.Services.AddMappingServices();

// Добавление поддержки NewtonsoftJson для Swagger
builder.Services.AddSwaggerGenNewtonsoftSupport();

// Регистрация контроллеров с поддержкой сериализации JSON
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    // Настройка форматирования JSON
    options.SerializerSettings.Formatting = Formatting.Indented;

    // Настройка обработки значений null
    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
});

// Добавление служб для работы с CORS
builder.Services.AddCorsServices();

// Создание приложения на основе настроек builder
await using var app = builder.Build();

// Создаем область для инициализации баз данных
using (var scope = app.Services.CreateScope())
{
    // Инициализация базы данных приложения
    await ApplicationDatabaseInitializer.InitAsync(scope.ServiceProvider);

    // Инициализация базы данных аутентификации
    await IdentityDatabaseInitializer.InitAsync(scope.ServiceProvider);
}

// Добавляем мидлварь обработки ошибок
app.UseMiddleware<ExceptionMiddleware>();

// Добавляем мидлварь аутентификации
app.UseAuthentication();

// Добавляем мидлварь авторизации
app.UseAuthorization();

// Использование Swagger для обслуживания документации по API
app.UseSwagger();

// Использование Swagger UI для предоставления интерактивной документации по API
app.UseSwaggerUI();

// Включение CORS
app.UseCors("DefaultPolicy");

// Маппим контроллеры
app.MapControllers();

// Запуск приложения
await app.RunAsync();