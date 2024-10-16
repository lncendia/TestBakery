using Bakery.Domain.Buns;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bakery.Infrastructure.Storage.Configurations;

/// <summary>
/// Класс конфигурации для сущности Bun.
/// </summary>
internal sealed class BunConfiguration : IEntityTypeConfiguration<Bun>
{
    /// <summary>
    /// Метод для настройки сущности Bun.
    /// </summary>
    /// <param name="builder">Построитель сущности</param>
    public void Configure(EntityTypeBuilder<Bun> builder)
    {
        // Указываем имя таблицы для сущности
        builder.ToTable("Buns");

        // Указываем первичный ключ для сущности
        builder.HasKey(bun => bun.Id);

        // Настраиваем дискриминатор для различения типов булочек
        builder.HasDiscriminator<string>("BunType")
            .HasValue<Baguette>("Baguette")
            .HasValue<Croissant>("Croissant")
            .HasValue<Loaf>("Loaf")
            .HasValue<Smetannik>("Smetannik")
            .HasValue<Pretzel>("Pretzel");

        // Устанавливаем максимальную длину для свойства "BunType"
        builder.Property("BunType").HasMaxLength(200);
    }
}