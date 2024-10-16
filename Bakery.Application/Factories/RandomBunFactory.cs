using Bakery.Domain.Abstractions;
using Bakery.Domain.Buns;

namespace Bakery.Application.Factories;

/// <summary>
/// Фабрика для создания случайных булочек.
/// </summary>
public class RandomBunFactory : IBunFactory
{
    /// <summary>
    /// Генератор случайных чисел.
    /// </summary>
    private static readonly Random Random = new();

    /// <summary>
    /// Создает случайную булочку.
    /// </summary>
    /// <returns>Объект Bun.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Выбрасывается, если тип булочки не определен.</exception>
    public Bun FactoryMethod()
    {
        // Генерируем случайный тип булочки
        var bunType = Random.Next(5);

        // Генерируем уникальный идентификатор для булочки
        var id = Guid.NewGuid();

        // Возвращаем случайную булочку в зависимости от типа
        return bunType switch
        {
            // Возвращает багет
            0 => new Baguette(id, 48, 35, 15),

            // Возвращает круассан
            1 => new Croissant(id, 24, 50, 12),

            // Возвращает батон
            2 => new Loaf(id, 48, 45, 24),

            // Возвращает крендель
            3 => new Pretzel(id, 36, 60, 20),

            // Возвращает сметанник
            4 => new Smetannik(id, 12, 80, 4),

            // Выбрасывает исключение, если тип булочки не определен
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}