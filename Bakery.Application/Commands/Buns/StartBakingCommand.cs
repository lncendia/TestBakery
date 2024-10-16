using Bakery.Application.DTOs.Buns;
using MediatR;

namespace Bakery.Application.Commands.Buns;

/// <summary>
/// Класс команды для начала выпечки булочек.
/// </summary>
public class StartBakingCommand : IRequest<BunsDto>
{
    /// <summary>
    /// Количество булочек, которые должны быть испечены.
    /// </summary>
    public required int Count { get; init; }
}
