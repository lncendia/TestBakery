namespace Bakery.Domain.Buns;

/// <summary>
/// Реализация класса Bun для сметанника.
/// </summary>
public class Smetannik(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    : Bun(id, sellHours, initialCost, controlSellHours)
{
    /// <summary>
    /// Метод вычисляет уменьшение стоимости сметанника с двойной скоростью.
    /// </summary>
    public override decimal CalculateCost(DateTime currentTime)
    {
        // Вычисляем количество часов, прошедших с момента выпечки
        var elapsedHours = (int)(currentTime - BakeTime).TotalHours;

        // Устанавливаем начальную стоимость булочки
        var currentCost = InitialCost;

        // Если еще не прошел контрольный срок продажи - возвращаем изначальную стоимость.
        if (elapsedHours < ControlSellHours) return currentCost;

        // Считаем два процента от начальной стоимости
        var reductionCost = InitialCost * 0.04m;
        
        // Уменьшаем стоимость на 2% от начальной стоимости каждый час от контрольного срока
        for (var i = ControlSellHours; i < elapsedHours && i < SellHours; i++)
        {
            // Уменьшение стоимости на 2% от начальной стоимости каждую продуктивную часть часа
            currentCost -= reductionCost;
        }

        // Возвращаем актуальную стоимость булочки
        return Math.Max(currentCost, 0);
    }
}