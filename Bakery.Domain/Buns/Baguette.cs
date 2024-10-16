namespace Bakery.Domain.Buns;

/// <summary>
/// Реализация класса Bun для багета.
/// </summary>
public class Baguette(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    : Bun(id, sellHours, initialCost, controlSellHours);