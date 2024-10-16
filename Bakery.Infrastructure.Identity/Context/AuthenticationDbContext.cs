﻿using Bakery.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Identity.Context;

/// <summary>
/// Контекст базы данных
/// </summary>
public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    /// <summary>
    /// Настраивает схему, необходимую для структуры идентификации.
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Вызываем базовую реализацию метода OnModelCreating
        base.OnModelCreating(builder);

        // Устанавливаем таблицу "Users" для сущности AppUser
        builder.Entity<User>(entity => entity.ToTable(name: "Users"));

        // Устанавливаем таблицу "Roles" для сущности AppRole
        builder.Entity<Role>(entity => entity.ToTable(name: "Roles"));

        // Устанавливаем таблицу "UserRoles" для сущности IdentityUserRole<Guid>
        builder.Entity<IdentityUserRole<Guid>>(entity => entity.ToTable(name: "UserRoles"));

        // Устанавливаем таблицу "UserClaims" для сущности IdentityUserClaim<Guid>
        builder.Entity<IdentityUserClaim<Guid>>(entity => entity.ToTable(name: "UserClaims"));

        // Устанавливаем таблицу "UserLogins" для сущности IdentityUserLogin<Guid>
        builder.Entity<IdentityUserLogin<Guid>>(entity => entity.ToTable("UserLogins"));

        // Устанавливаем таблицу "UserTokens" для сущности IdentityUserToken<Guid>
        builder.Entity<IdentityUserToken<Guid>>(entity => entity.ToTable("UserTokens"));

        // Устанавливаем таблицу "RoleClaims" для сущности IdentityRoleClaim<Guid>
        builder.Entity<IdentityRoleClaim<Guid>>(entity => entity.ToTable("RoleClaims"));
        
        // Найти конфигурацию сущности ApplicationUser и изменить настройку индекса для UserName
        var userEntity = builder.Entity<User>();
        var index = userEntity.Metadata.FindIndex(nameof(User.NormalizedUserName));
        if (index != null)
        {
            // Удаляем существующий уникальный индекс
            userEntity.Metadata.RemoveIndex(index.Properties);
        }

        // Можно добавить неуникальный индекс, если он нужен для оптимизации запросов
        userEntity.HasIndex(u => u.NormalizedUserName).IsUnique(false);
    }
}