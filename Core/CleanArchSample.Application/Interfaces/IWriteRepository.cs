using CleanArchSample.Domain.Common;

namespace CleanArchSample.Application.Interfaces
{
    public interface IWriteRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IList<T> entities, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;
        Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IList<T> entities, CancellationToken cancellationToken = default);
    }
}
