namespace Bakery.Application.DTOs.Buns;

/// <summary>
/// Класс, представляющий данные булочки.
/// </summary>
public class BunDto
{
    /// <summary>
    /// Идентификатор булочки.
    /// </summary>
    public required Guid Id { get; init; }
    
    /// <summary>
    /// Тип булочки.
    /// </summary>
    public required string Type { get; init; }

    /// <summary>
    /// Время выпечки булочки.
    /// </summary>
    public required DateTime BakeTime { get; init; }

    /// <summary>
    /// Начальная стоимость булочки.
    /// </summary>
    public required decimal InitialCost { get; init; }

    /// <summary>
    /// Текущая стоимость булочки.
    /// </summary>
    public required decimal CurrentCost { get; init; }

    /// <summary>
    /// Прогнозируемая стоимость булочки.
    /// </summary>
    public PredictedCostDto? PredictedCost { get; init; }
}