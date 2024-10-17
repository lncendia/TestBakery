using Bakery.Application.Commands.Buns;
using Bakery.Application.DTOs.Buns;
using Bakery.Application.Extenstions;
using Bakery.Domain.Abstractions;
using Bakery.Domain.Buns;
using MediatR;

namespace Bakery.Application.CommandsHandlers.Buns;

/// <summary>
/// Обработчик команды для начала выпечки булочек.
/// </summary>
/// <param name="repository">Репозиторий булочек.</param>
/// <param name="factory">Фабрика булочек.</param>
public class StartBakingCommandHandler(IBunFactory factory, IBunRepository repository)
    : IRequestHandler<StartBakingCommand, BunsDto>
{
    /// <summary>
    /// Обрабатывает команду для начала выпечки булочек.
    /// </summary>
    /// <param name="request">Команда для начала выпечки булочек.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Объект BunsDto, содержащий информацию о выпеченных булочках.</returns>
    public async Task<BunsDto> Handle(StartBakingCommand request, CancellationToken cancellationToken)
    {
        // Создаем массив для хранения выпеченных булочек
        var buns = new Bun[request.Count];

        // Создаем булочки с помощью фабрики
        for (var i = 0; i < request.Count; i++) buns[i] = factory.FactoryMethod();

        // Добавляем булочки в репозиторий
        await repository.AddRangeAsync(buns, cancellationToken);

        // Получаем общее количество булочек в репозитории
        var count = await repository.CountAsync(cancellationToken);

        // Создаем массив для хранения DTO булочек
        var mappedBuns = new BunDto[buns.Length];

        // Получаем текущее время
        var now = DateTime.UtcNow;

        // Преобразуем булочки в DTO в обратном порядке
        for (var i = buns.Length - 1; i >= 0; i--) mappedBuns[buns.Length - i - 1] = BunMapper.Map(buns[i], now);

        // Возвращаем объект BunsDto, содержащий информацию о выпеченных булочках
        return new BunsDto { Buns = mappedBuns, TotalCount = count };
    }
}