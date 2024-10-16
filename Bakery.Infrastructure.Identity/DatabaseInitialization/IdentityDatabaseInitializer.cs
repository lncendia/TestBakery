using Bakery.Application.Models;
using Bakery.Infrastructure.Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bakery.Infrastructure.Identity.DatabaseInitialization;

/// <summary>
/// Класс для инициализации начальных данных в базу данных
/// </summary>
public static class IdentityDatabaseInitializer
{
    /// <summary>
    /// Инициализация начальных данных в базу данных
    /// </summary>
    /// <param name="serviceProvider">Провайдер DI.</param>
    public static async Task InitAsync(IServiceProvider serviceProvider)
    {
        // Получаем контекст базы данных
        var context = serviceProvider.GetRequiredService<AuthenticationDbContext>();

        //обновляем базу данных
        await context.Database.MigrateAsync();

        // Получаем экземпляр сервиса UserManager<AppUser> из провайдера служб scopeServiceProvider.
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

        // Получаем экземпляр сервиса RoleManager<AppRole> из провайдера служб scopeServiceProvider.
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        // Определение константных строковых переменных для почты пекаря.
        const string bakerEmail = "coolbaker23@gmail.com";

        // Проверяем, существует ли роль "Baker". Если нет, то создаем новую роль "Baker".
        if (await roleManager.FindByNameAsync("Baker") == null)
        {
            await roleManager.CreateAsync(new Role { Name = "Baker" });
        }

        // Проверяем, существует ли пользователь с почтой bakerEmail.
        if (await userManager.FindByEmailAsync(bakerEmail) == null)
        {
            // Создаем нового пользователя
            var admin = new User
            {
                Email = bakerEmail,
                UserName = "BestBaker23",
                EmailConfirmed = true
            };

            // Создаем пользователя
            var result = await userManager.CreateAsync(admin, "BaKe_R23");

            // Если успешно - добавляем пользователя в роль "Baker"
            if (result.Succeeded)
            {
                //добавляем его в роль
                await userManager.AddToRoleAsync(admin, "Baker");
            }
        }
    }
}