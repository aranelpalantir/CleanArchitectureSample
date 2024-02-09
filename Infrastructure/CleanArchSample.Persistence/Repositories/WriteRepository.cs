using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext _dbContext;

        public WriteRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table => _dbContext.Set<T>();
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
            var entity = await Table.FindAsync(new object[] { id }, cancellationToken);
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
