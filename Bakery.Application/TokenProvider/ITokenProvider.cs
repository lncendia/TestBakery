using System.Security.Claims;

namespace Bakery.Application.TokenProvider;

/// <summary>
/// Интерфейс для предоставления токенов доступа.
/// </summary>
public interface ITokenProvider
{
    /// <summary>
    /// Генерирует токен доступа на основе предоставленных утверждений.
    /// </summary>
    /// <param name="claims">Коллекция утверждений для генерации токена.</param>
    /// <returns>Строка, представляющая сгенерированный токен доступа.</returns>
    string GenerateAccess(IEnumerable<Claim> claims);
}