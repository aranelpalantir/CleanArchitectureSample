using CleanArchSample.Domain.Primitives;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal abstract class BaseRepository<T>(AppDbContext dbContext) : IRepository<T>
        where T : class, IBaseEntity, new()
    {
        protected readonly AppDbContext DbContext = dbContext;
        private DbSet<T> Table => DbContext.Set<T>();

        public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await Table.FindAsync([id], cancellationToken);
        }

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
