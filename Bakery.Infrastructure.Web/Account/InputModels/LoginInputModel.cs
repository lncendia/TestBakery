namespace Bakery.Infrastructure.Web.Account.InputModels;

/// <summary>
/// Модель входа в систему
/// </summary>
public class LoginInputModel
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string? Email { get; init; }

    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; init; }
}