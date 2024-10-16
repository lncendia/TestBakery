namespace Bakery.Domain.Buns;

/// <summary>
/// Абстрактный класс Bun, представляющий основные характеристики и поведение булочки.
/// </summary>
public abstract class Bun
{
    /// <summary>
    /// Идентификатор булочки.
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Тип булочки.
    /// </summary>
    public string Type => GetType().Name;

    /// <summary>
    /// Время выпечки булочки.
    /// </summary>
    public DateTime BakeTime { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// Количество часов, в течение которых булочка должна быть продана.
    /// </summary>
    public int SellHours { get; private set; }

    /// <summary>
    /// Начальная стоимость булочки.
    /// </summary>
    public decimal InitialCost { get; private set; }
    
    /// <summary>
    /// Контрольный срок продажи.
    /// </summary>
    public int ControlSellHours { get; private set; }

    /// <summary>
    /// Максимальная начальная стоимость булочки.
    /// </summary>
    private const int MaxInitialCost = 100;

    /// <summary>
    /// Максимальное количество часов, в течение которых булочка должна быть продана.
    /// </summary>
    private const int MaxSellHours = 100;

    /// <summary>
    /// Максимальное количество часов для контрольного срока продажи.
    /// </summary>
    private const int MaxControlHours = 100;

    /// <summary>
    /// Конструктор, инициализирующий основные свойства булочки.
    /// </summary>
    /// <param name="id">Уникальный идентификатор булочки.</param>
    /// <param name="sellHours">Количество часов, в течение которых булочка должна быть продана.</param>
    /// <param name="initialCost">Начальная стоимость булочки.</param>
    /// <param name="controlSellHours">Контрольный срок продажи.</param>
    protected Bun(Guid id, int sellHours, decimal initialCost, int controlSellHours)
    {
        // Проверка начальной стоимости
        if (initialCost is <= 0 or > MaxInitialCost)
        {
            // Выбрасываем исключение, если начальная стоимость вне допустимого диапазона
            throw new ArgumentOutOfRangeException(nameof(initialCost),
                "Initial cost must be greater than 0 and less than or equal to 100.");
        }

        // Проверка количества часов продажи
        if (sellHours is <= 0 or >= MaxSellHours)
        {
            // Выбрасываем исключение, если количество часов продажи вне допустимого диапазона
            throw new ArgumentOutOfRangeException(nameof(sellHours),
                "Sell hours must be greater than 0 and less than 100.");
        }
        
        // Проверка контрольного срока продажи
        if (controlSellHours is <= 0 or >= MaxControlHours)
        {
            // Выбрасываем исключение, если контрольный срок продажи вне допустимого диапазона
            throw new ArgumentOutOfRangeException(nameof(controlSellHours),
                "Control sell hours must be greater than 0 and less than 100.");
        }

        // Если контрольный срок продажи больше чем количество часов продажи
        if (controlSellHours >= sellHours)
        {
            // Выбрасываем исключение, если контрольный срок превышает количество часов продажи
            throw new ArgumentException("Control sell hours cannot exceed the total sell hours.", nameof(controlSellHours));
        }

        // Устанавливаем контрольный срок продажи
        ControlSellHours = controlSellHours;

        // Устанавливаем количество часов продажи
        SellHours = sellHours;

        // Устанавливаем начальную стоимость
        InitialCost = initialCost;

        // Устанавливаем уникальный идентификатор
        Id = id;
    }

    /// <summary>
    /// Виртуальный метод, вычисляющий стоимость булочки в указанное время.
    /// </summary>
    /// <param name="currentTime">Текущее время.</param>
    /// <returns>Актуальная стоимость булочки.</returns>
    public virtual decimal CalculateCost(DateTime currentTime)
    {
        // Вычисляем количество часов, прошедших с момента выпечки
        var elapsedHours = (int)(currentTime - BakeTime).TotalHours;
        
        // Устанавливаем начальную стоимость булочки
        var currentCost = InitialCost;

        // Если еще не прошел контрольный срок продажи - возвращаем изначальную стоимость.
        if (elapsedHours < ControlSellHours) return currentCost;

        // Считаем два процента от начальной стоимости
        var reductionCost = InitialCost * 0.02m;
        
        // Уменьшаем стоимость на 2% от начальной стоимости каждый час от контрольного срока
        for (var i = ControlSellHours; i < elapsedHours && i < SellHours; i++)
        {
            // Уменьшение стоимости на 2% от начальной стоимости каждую продуктивную часть часа
            currentCost -= reductionCost;
        }

        // Возвращаем актуальную стоимость булочки
        return Math.Max(currentCost, 0);
    }

    /// <summary>
    /// Виртуальный метод, предсказывающий время следующего изменения стоимости булочки.
    /// </summary>
    /// <param name="currentTime">Текущее время.</param>
    /// <returns>Время следующего изменения стоимости булочки, или null, если изменений больше не будет.</returns>
    public virtual DateTime? PredictNextPriceChangeTime(DateTime currentTime)
    {
        // Вычисляем время контрольного срока продажи
        var controlDate = BakeTime.AddHours(ControlSellHours);

        // Если текущее время меньше времени контрольного срока продажи - возвращаем контрольный срок продажи плюс 1 час
        if (currentTime < controlDate) return controlDate.AddHours(1);
        
        // Вычисляем количество часов, прошедших с момента выпечки
        var elapsedHours = (int)(currentTime - BakeTime).TotalHours;

        // Если прошло больше или равно количеству часов продажи минус один, возвращаем null
        if (elapsedHours >= SellHours - 1) return null;

        // Если есть еще хотя бы один час срока жизни булочки, возвращаем время на час больше
        return BakeTime.AddHours(elapsedHours + 1);
    }
}