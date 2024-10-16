namespace Bakery.Domain.Buns;

/// <summary>
/// Реализация класса Bun для круассана.
/// </summary>
public class Croissant(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    : Bun(id, sellHours, initialCost, controlSellHours);