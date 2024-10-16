namespace Bakery.Application.Exceptions;

/// <summary>
/// Исключение, возникающее, если пользователь заблокирован.
/// </summary>
public class UserLockoutException(DateTimeOffset endTime) : Exception("User is locked.")
{
    /// <summary>
    /// Время окончания блокировки
    /// </summary>
    public DateTimeOffset EndTime { get; } = endTime;
}
