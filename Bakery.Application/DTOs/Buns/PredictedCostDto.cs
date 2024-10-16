namespace Bakery.Application.DTOs.Buns;

/// <summary>
/// Класс, представляющий данные прогнозируемой стоимости.
/// </summary>
public class PredictedCostDto
{
    /// <summary>
    /// Дата прогноза.
    /// </summary>
    public required TimeSpan Date { get; init; }

    /// <summary>
    /// Прогнозируемая стоимость.
    /// </summary>
    public required decimal Cost { get; init; }
}
