using Bakery.Domain.Buns;

namespace Bakery.Domain.Abstractions;

/// <summary> 
/// Интерфейс репозитория для хранения булочек. 
/// </summary> 
public interface IBunRepository
{
    /// <summary> 
    /// Асинхронно добавляет новую булочку. 
    /// </summary> 
    /// <param name="entity">Булочка для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task AddAsync(Bun entity, CancellationToken cancellationToken = default);

    /// <summary> 
    /// Асинхронно добавляет новые булочки. 
    /// </summary>
    /// <param name="entities">Булочки для добавления.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task AddRangeAsync(Bun[] entities, CancellationToken cancellationToken = default);

    /// <summary> 
    /// Асинхронно удаляет булочку по ее ключу. 
    /// </summary>
    /// <param name="id">Ключ булочки для удаления.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary> 
    /// Асинхронно получает булочку по ее ключу. 
    /// </summary> 
    /// <param name="id">Ключ булочки для получения.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>булочка с указанным ключом.</returns> 
    Task<Bun?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary> 
    /// Асинхронно получает список булочек. 
    /// </summary>
    /// <param name="skip">Количество пропускаемых булочек.</param>
    /// <param name="take">Количество выбираемых булочек.</param>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Список булочек.</returns> 
    Task<IReadOnlyCollection<Bun>> GetRangeAsync(int? skip = null, int? take = null,
        CancellationToken cancellationToken = default);

    /// <summary> 
    /// Возвращает количество булочек. 
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены операции</param> 
    /// <returns>Количество булочек, удовлетворяющих условиям поиска.</returns> 
    Task<int> CountAsync(CancellationToken cancellationToken = default);
}