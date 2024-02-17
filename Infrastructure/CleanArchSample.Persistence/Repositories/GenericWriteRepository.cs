using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Primitives;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class GenericWriteRepository<T>(AppDbContext dbContext) : IGenericWriteRepository<T>
        where T : class, IEntityBase, new()
    {
        private DbSet<T> Table => dbContext.Set<T>();
        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Table.AddAsync(entity, cancellationToken);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Table.AddRangeAsync(entities, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Table.Update(entity), cancellationToken);
        }

        public async Task RemoveAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            var entity = await Table.FindAsync([id], cancellationToken) ?? throw new EntityNotFoundException();
            await Task.Run(() => Table.Remove(entity), cancellationToken);
        }

        public async Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Table.Remove(entity), cancellationToken);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => Table.RemoveRange(entities), cancellationToken);
        }
    }
}
