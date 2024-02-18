using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity, new()
    {
        Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
