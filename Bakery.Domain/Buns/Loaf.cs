namespace Bakery.Domain.Buns;

/// <summary>
/// Реализация класса Bun для батона.
/// </summary>
public class Loaf(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    : Bun(id, sellHours, initialCost, controlSellHours);