namespace Bakery.Domain.Buns;

/// <summary>
/// Реализация класса Bun для кренделя.
/// </summary>
public class Pretzel(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    : Bun(id, sellHours, initialCost, controlSellHours)
{
    /// <inheritdoc/>
    /// <summary>
    /// Метод вычисляет стоимость кренделя с учетом контрольного срока.
    /// </summary>
    public override decimal CalculateCost(DateTime currentTime)
    {
        // Вычисляем количество часов, прошедших с момента выпечки
        var elapsedHours = (int)(currentTime - BakeTime).TotalHours;

        // Устанавливаем начальную стоимость кренделя
        var currentCost = InitialCost;

        // Если прошло больше или равно контрольному сроку продажи
        if (elapsedHours >= ControlSellHours)
        {
            // Уменьшение стоимости вдвое после контрольного срока
            currentCost /= 2;
        }

        // Возвращаем актуальную стоимость кренделя
        return currentCost;
    }

    /// <inheritdoc/>
    /// <summary>
    /// Метод для прогнозирования следующего изменения цены.
    /// </summary>
    public override DateTime? PredictNextPriceChangeTime(DateTime currentTime)
    {
        // Вычисляем время контрольного срока продажи
        var controlDate = BakeTime.AddHours(ControlSellHours);

        // Если текущее время больше или равно времени контрольного срока продажи, возвращаем null
        if (currentTime >= controlDate) return null;

        // Возвращаем время контрольного срока продажи
        return controlDate;
    }
}