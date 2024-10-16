using AutoMapper;
using Bakery.Application.Commands.Buns;
using Bakery.Application.DTOs.Buns;
using Bakery.Application.Queries.Buns;
using Bakery.Infrastructure.Web.Buns.InputModels;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Infrastructure.Web.Buns.Controllers;

/// <summary>
/// Контроллер отвечающий за работу с булочками
/// </summary>
/// <param name="mediator">Медиатор.</param>
/// <param name="mapper">Маппер.</param>
[ApiController]
[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Baker")]
public class BunController(ISender mediator, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получить список булочек.
    /// </summary>
    /// <param name="model">Данные запроса.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param> 
    /// <response code="400">Невалидный запрос.</response>
    /// <response code="500">Возникла ошибка на сервере.</response>
    [HttpGet]
    public async Task<BunsDto> GetBuns([FromQuery] GetBunsInputModel model,
        CancellationToken cancellationToken)
    {
        // Создаем запрос
        var query = mapper.Map<GetBunsQuery>(model);

        // Отправляем запрос на получение булочек
        return await mediator.Send(query, cancellationToken);
    }

    /// <summary>
    /// Запустить выпечку.
    /// </summary>
    /// <param name="model">Данные для запуска выпечки.</param>
    /// <param name="cancellationToken">Токен для отмены операции.</param> 
    /// <response code="400">Некорректные входные данные или невалидный запрос.</response>
    /// <response code="500">Возникла ошибка на сервере.</response>
    [HttpPost]
    public async Task<BunsDto> StartBaking(StartBakingInputModel model, CancellationToken cancellationToken)
    {
        // Создаем команду
        var command = mapper.Map<StartBakingCommand>(model);

        // Отправляем запрос на запуск выпечки
        return await mediator.Send(command, cancellationToken);
    }
}