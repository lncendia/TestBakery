using System.Linq.Expressions;
using Bakery.Domain.Abstractions;
using Bakery.Domain.Buns;
using Bakery.Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Infrastructure.Storage.Repositories;

/// <summary> 
/// Реализация репозитория для хранения булочек. 
/// </summary> 
public class BunRepository(ApplicationDbContext context) : IBunRepository
{
    /// <summary>
    /// Предикат для определения, продается ли ещё булочка.
    /// </summary>
    private static readonly Expression<Func<Bun, bool>> SellPredicate = b => b.BakeTime.AddHours(b.SellHours) > DateTime.UtcNow;

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно добавляет новую булочку. 
    /// </summary> 
    public async Task AddAsync(Bun entity, CancellationToken cancellationToken = default)
    {
        // Добавляем сущность в контекст
        await context.AddAsync(entity, cancellationToken);

        // Асинхронное сохранение изменений в контексте
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно добавляет новые булочки. 
    /// </summary>
    public async Task AddRangeAsync(Bun[] entities, CancellationToken cancellationToken = default)
    {
        // Добавляем сущности в контекст
        await context.Set<Bun>().AddRangeAsync(entities, cancellationToken);

        // Асинхронное сохранение изменений в контексте
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно удаляет булочку по ее ключу. 
    /// </summary>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Получение сущности
        var entity = await context.Set<Bun>()
            .FirstAsync(report => report.Id == id, cancellationToken: cancellationToken);

        // Удаление сущности
        context.Remove(entity);

        // Асинхронное сохранение изменений в контексте
        await context.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно получает булочку по ее ключу. 
    /// </summary> 
    public Task<Bun?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        // Получение сущности из контекста.
        return context.Set<Bun>()
            .Where(SellPredicate)
            .FirstOrDefaultAsync(bun => bun.Id == id, cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Асинхронно получает список булочек. 
    /// </summary>
    public async Task<IReadOnlyCollection<Bun>> GetRangeAsync(int? skip = null, int? take = null,
        CancellationToken cancellationToken = default)
    {
        // Создаем базовый запрос для получения булочек, которые еще не проданы
        var query = context.Set<Bun>()
            .Where(SellPredicate)
            .OrderByDescending(b => b.BakeTime)
            .AsQueryable();

        // Если указан параметр skip, пропускаем указанное количество элементов
        if (skip.HasValue) query = query.Skip(skip.Value);

        // Если указан параметр take, берем указанное количество элементов
        if (take.HasValue) query = query.Take(take.Value);

        // Возвращаем результат запроса в виде массива
        return await query.ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc/>
    /// <summary> 
    /// Возвращает количество булочек. 
    /// </summary>
    public Task<int> CountAsync(CancellationToken cancellationToken)
    {
        // Получение и возврат количества булочек в базе данных
        return context.Set<Bun>()
            .Where(SellPredicate)
            .CountAsync(cancellationToken);
    }
}