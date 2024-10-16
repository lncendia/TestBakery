using Bakery.Domain.Buns;

namespace Bakery.Domain.Abstractions;

/// <summary>
/// Интерфейс для фабрики булочек.
/// </summary>
public interface IBunFactory
{
    /// <summary>
    /// Создает новую булочку.
    /// </summary>
    /// <returns>Объект Bun.</returns>
    Bun FactoryMethod();
}
