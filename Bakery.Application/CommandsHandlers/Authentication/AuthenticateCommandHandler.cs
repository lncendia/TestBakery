using Bakery.Application.Commands.Authentication;
using Bakery.Application.Exceptions;
using Bakery.Application.Models;
using Bakery.Application.TokenProvider;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Bakery.Application.CommandsHandlers.Authentication;

/// <summary>
/// Класс обработчика команды аутентификации пользователя по паролю.
/// </summary>
/// <param name="userManager">Менеджер пользователей, предоставленный ASP.NET Core Identity.</param>
public class AuthenticateCommandHandler(
    UserManager<User> userManager,
    IUserClaimsPrincipalFactory<User> factory,
    ITokenProvider provider) : IRequestHandler<AuthenticateCommand, string>
{
    /// <summary>
    /// Обработка команды аутентификации пользователя по паролю.
    /// </summary>
    /// <param name="request">Запрос на аутентификацию пользователя.</param>
    /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
    /// <returns>Объект пользователя в случае успешной аутентификации.</returns>
    /// <exception cref="UserNotFoundException">Вызывается, если пользователь не найден.</exception>
    /// <exception cref="UserLockoutException">Вызывается, если пользователь заблокирован.</exception>
    /// <exception cref="InvalidPasswordException">Вызывается, если валидация пароля не прошла.</exception>
    public async Task<string> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        // Получаем пользователя по его электронной почте.
        var user = await userManager.FindByEmailAsync(request.Email);

        // Если пользователь не найден, вызываем исключение UserNotFoundException.
        if (user == null) throw new UserNotFoundException();

        // Проверяем заблокирован ли пользователь 
        if (await userManager.IsLockedOutAsync(user))
        {
            // Если пользователь заблокирован, вызываем исключение UserLockoutException.
            throw new UserLockoutException(user.LockoutEnd!.Value);
        }

        // Проверяем правильность введенного пароля.
        var success = await userManager.CheckPasswordAsync(user, request.Password);

        // Если пароль неверный
        if (!success)
        {
            // Инкрементируем счетчик неудачных попыток
            await userManager.AccessFailedAsync(user);

            // Вызываем исключение InvalidPasswordException.
            throw new InvalidPasswordException();
        }

        // Cбрасываем счетчик неудачных попыток входа
        await userManager.ResetAccessFailedCountAsync(user);

        var principal = await factory.CreateAsync(user);

        return provider.GenerateAccess(principal.Claims);
    }
}