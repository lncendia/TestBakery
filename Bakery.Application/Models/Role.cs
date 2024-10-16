using Microsoft.AspNetCore.Identity;

namespace Bakery.Application.Models;

/// <summary>
/// Класс, представляющий роль пользователя.
/// </summary>
public class Role : IdentityRole<Guid>;