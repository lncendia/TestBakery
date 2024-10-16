using MediatR;

namespace Bakery.Application.Commands.Authentication;

/// <summary>
/// Команда для аутентификации пользователя по паролю.
/// </summary>
public class AuthenticateCommand : IRequest<string>
{
    /// <summary>
    /// Электронная почта пользователя.
    /// </summary>
    public required string Email { get; init; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public required string Password { get; init; }
}