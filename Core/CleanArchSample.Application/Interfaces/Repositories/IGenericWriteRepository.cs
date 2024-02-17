using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Application.Interfaces.Repositories
{
    public interface IGenericWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
