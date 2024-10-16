using Bakery.Application.DTOs.Buns;
using Bakery.Application.Extenstions;
using Bakery.Application.Queries.Buns;
using Bakery.Domain.Abstractions;
using MediatR;

namespace Bakery.Application.QueriesHandlers.Buns;

/// <summary>
/// Обработчик запроса для получения данных о булочках.
/// </summary>
/// <param name="repository">Репозиторий булочек.</param>
public class GetBunsQueryHandler(IBunRepository repository) : IRequestHandler<GetBunsQuery, BunsDto>
{
    /// <summary>
    /// Обрабатывает запрос для получения данных о булочках.
    /// </summary>
    /// <param name="request">Запрос для получения данных о булочках.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Объект BunsDto, содержащий информацию о булочках.</returns>
    public async Task<BunsDto> Handle(GetBunsQuery request, CancellationToken cancellationToken)
    {
        // Получаем общее количество булочек в репозитории
        var count = await repository.CountAsync(cancellationToken);

        // Если количество булочек равно нулю, возвращаем пустой объект BunsDto
        if (count == 0) return new BunsDto { Buns = Array.Empty<BunDto>(), TotalCount = 0 };

        // Получаем диапазон булочек из репозитория
        var buns = await repository.GetRangeAsync(request.Offset, request.Limit, cancellationToken);

        // Создаем массив для хранения DTO булочек
        var mappedBuns = new BunDto[buns.Count];

        // Получаем текущее время
        var now = DateTime.UtcNow;

        // Преобразуем булочки в DTO
        for (var i = 0; i < buns.Count; i++)
        {
            // Получаем булочку из коллекции
            var bun = buns.ElementAt(i);

            // Преобразуем булочку в DTO
            mappedBuns[i] = BunMapper.Map(bun, now);
        }

        // Возвращаем объект BunsDto, содержащий информацию о булочках
        return new BunsDto { Buns = mappedBuns, TotalCount = count };
    }
}