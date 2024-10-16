namespace Bakery.Application.DTOs.Buns;

/// <summary>
/// Класс, представляющий коллекцию данных булочек.
/// </summary>
public class BunsDto
{
    /// <summary>
    /// Коллекция данных булочек.
    /// </summary>
    public required IReadOnlyCollection<BunDto> Buns { get; init; }

    /// <summary>
    /// Общее количество булочек.
    /// </summary>
    public required int TotalCount { get; init; }
}
