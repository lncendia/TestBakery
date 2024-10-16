using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bakery.Application.TokenProvider;
using Microsoft.IdentityModel.Tokens;

namespace Bakery.Infrastructure.Identity.TokenProvider;

/// <summary>
/// Класс, предоставляющий токены доступа.
/// </summary>
public class TokenProvider : ITokenProvider
{
    /// <summary>
    /// Обработчик токенов JWT.
    /// </summary>
    private readonly JwtSecurityTokenHandler _handler = new();

    /// <summary>
    /// Издатель токена.
    /// </summary>
    private readonly string _issuer;

    /// <summary>
    /// Аудитория токена.
    /// </summary>
    private readonly string _audience;

    /// <summary>
    /// Учетные данные для подписи токена.
    /// </summary>
    private readonly SigningCredentials _credentials;

    /// <summary>
    /// Время жизни токена доступа.
    /// </summary>
    private readonly TimeSpan _accessTokenLifetime;

    /// <summary>
    /// Конструктор класса TokenProvider.
    /// </summary>
    /// <param name="issuer">Издатель токена.</param>
    /// <param name="audience">Аудитория токена.</param>
    /// <param name="secretKey">Секретный ключ для подписи токена.</param>
    /// <param name="accessTokenLifetime">Время жизни токена доступа.</param>
    public TokenProvider(string issuer, string audience, string secretKey, TimeSpan accessTokenLifetime)
    {
        // Устанавливаем издателя токена
        _issuer = issuer;

        // Устанавливаем аудиторию токена
        _audience = audience;

        // Создаем симметричный ключ безопасности на основе секретного ключа
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        // Создаем учетные данные для подписи токена
        _credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Устанавливаем время жизни токена доступа
        _accessTokenLifetime = accessTokenLifetime;
    }

    /// <summary>
    /// Генерирует токен доступа на основе предоставленных утверждений.
    /// </summary>
    /// <param name="claims">Коллекция утверждений для генерации токена.</param>
    /// <returns>Строка, представляющая сгенерированный токен доступа.</returns>
    public string GenerateAccess(IEnumerable<Claim> claims)
    {
        // Создаем токен JWT
        var accessToken = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.Add(_accessTokenLifetime),
            signingCredentials: _credentials);

        // Возвращаем строку, представляющую сгенерированный токен доступа
        return _handler.WriteToken(accessToken);
    }
}