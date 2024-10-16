using Microsoft.AspNetCore.Identity;

namespace Bakery.Application.Models;

/// <summary>
/// Класс, представляющий пользователя.
/// </summary>
public class User : IdentityUser<Guid>;