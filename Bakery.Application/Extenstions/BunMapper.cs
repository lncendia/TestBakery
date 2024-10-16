using Bakery.Application.DTOs.Buns;
using Bakery.Domain.Buns;

namespace Bakery.Application.Extenstions;

/// <summary>
/// Класс для преобразования объектов Bun в объекты BunDto.
/// </summary>
public static class BunMapper
{
    /// <summary>
    /// Преобразует объект Bun в объект BunDto.
    /// </summary>
    /// <param name="bun">Объект Bun для преобразования.</param>
    /// <param name="now">Текущее время.</param>
    /// <returns>Объект BunDto.</returns>
    public static BunDto Map(Bun bun, DateTime now)
    {
        // Инициализируем прогнозируемую стоимость как null
        PredictedCostDto? predictedCost = null;

        // Получаем время следующего изменения стоимости
        var predictedCostChange = bun.PredictNextPriceChangeTime(now);

        // Если время следующего изменения стоимости определено
        if (predictedCostChange != null)
        {
            // Создаем объект PredictedCostDto
            predictedCost = new PredictedCostDto
            {
                // Устанавливаем время следующего изменения стоимости
                Date = predictedCostChange.Value - now,

                // Устанавливаем прогнозируемую стоимость
                Cost = bun.CalculateCost(predictedCostChange.Value)
            };
        }

        // Возвращаем объект BunDto
        return new BunDto
        {
            // Устанавливаем идентификатор булочки
            Id = bun.Id,

            // Устанавливаем тип булочки
            Type = bun.Type,

            // Устанавливаем время выпечки булочки
            BakeTime = bun.BakeTime,

            // Устанавливаем начальную стоимость булочки
            InitialCost = bun.InitialCost,

            // Устанавливаем текущую стоимость булочки
            CurrentCost = bun.CalculateCost(now),

            // Устанавливаем прогнозируемую стоимость булочки
            PredictedCost = predictedCost
        };
    }
}
