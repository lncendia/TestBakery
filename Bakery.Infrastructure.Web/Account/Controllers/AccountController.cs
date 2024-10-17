using AutoMapper;
using Bakery.Application.Commands.Authentication;
using Bakery.Infrastructure.Web.Account.InputModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Infrastructure.Web.Account.Controllers;

/// <summary>
/// Контроллер для прохождения аутентификации.
/// </summary>
/// <param name="mediator">Медиатор.</param>
/// <param name="mapper">Маппер.</param>
[ApiController]
[Route("[controller]/[action]")]
public class AccountController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Обработка аутентификации
    /// </summary>
    /// <param name="model">Данные для входа.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос.</response>
    /// <response code="500">Возникла ошибка на сервере.</response>
    [HttpPost]
    public async Task<string> Token(LoginInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<AuthenticateCommand>(model);

        // Отправляем запрос на аутентификацию
        return await mediator.Send(command, cancellationToken);
    }
}