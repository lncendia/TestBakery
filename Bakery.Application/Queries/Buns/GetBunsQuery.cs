using Bakery.Application.DTOs.Buns;
using MediatR;

namespace Bakery.Application.Queries.Buns;

/// <summary>
/// Класс запроса для получения данных о булочках.
/// </summary>
public class GetBunsQuery : IRequest<BunsDto>
{
    /// <summary>
    /// Максимальное количество булочек, которое должно быть возвращено.
    /// </summary>
    public required int Limit { get; init; }

    /// <summary>
    /// Смещение, с которого начинается возврат данных о булочках.
    /// </summary>
    public required int Offset { get; init; }
}
