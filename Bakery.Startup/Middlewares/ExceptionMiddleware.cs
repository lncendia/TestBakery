using System.Diagnostics;
using System.Net;
using Bakery.Application.Exceptions;

namespace Bakery.Startup.Middlewares;

/// <summary>
/// Промежуточное ПО для обработки исключений.
/// </summary>
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    /// <summary>
    /// Выполнение обработки запроса.
    /// </summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    /// <returns>Асинхронная задача.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Вызов следующего промежуточного ПО в конвейере обработки запроса.
            await next(context);
        }
        catch (Exception exception)
        {
            // Сообщение об ошибке
            string? message;

            // Статус код по умолчанию (400)
            var statusCode = (int)HttpStatusCode.BadRequest;

            // Секция в стандарте
            var section = "15.5.1";

            // Формируем текст ошибки в зависимости от типа ошибки
            switch (exception)
            {
                // Неверный пароль
                case InvalidPasswordException:
                    message = "Указанный пароль неверен.";
                    break;
                // Пользователь заблокирован
                case UserLockoutException ex:
                    var time = ex.EndTime - DateTimeOffset.UtcNow;
                    message = $"Пользователь заблокирован. Повторите попытку через {time.Minutes:00}:{time.Seconds:00}.";
                    break;
                // Пользователь не найден
                case UserNotFoundException:
                    message = "Пользователь не найден.";
                    break;

                // Действие по умолчанию
                default:

                    // Устанавливаем статус код 500
                    statusCode = (int)HttpStatusCode.InternalServerError;

                    // Устанавливаем секцию на этот статус код в стандарте
                    section = "15.6.1";

                    // Устанавливаем сообщение ошибки
                    message = exception.Message;

                    // Логгируем исключение
                    logger.LogError(exception, "Возникла ошибка при обработке запроса");
                    break;
            }

            // Установка статуса кода ответа.
            context.Response.StatusCode = statusCode;

            // Формирование объекта ошибки для отправки клиенту.
            var errorResponse = new
            {
                // Ошибка
                errors = new { Error = message },

                // Ссылка на стандарт
                type = $"https://tools.ietf.org/html/rfc9110#section-{section}",

                // Заголовок
                title = "One or more errors occurred.",

                // Статус запроса
                status = context.Response.StatusCode,

                // Уникальный идентификатор запроса
                traceId = Activity.Current?.Id ?? context.TraceIdentifier
            };

            // Отправка объекта ошибки в формате JSON.
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}