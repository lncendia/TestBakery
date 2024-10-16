namespace Bakery.Infrastructure.Web.Buns.InputModels;

/// <summary>
/// Модель входных данных для получения контактов.
/// </summary>
public class GetBunsInputModel
{
    /// <summary>
    /// Лимит контактов.
    /// </summary>
    public int Limit { get; init; } = 100;

    /// <summary>
    /// Смещение.
    /// </summary>
    public int? Offset { get; init; }
}