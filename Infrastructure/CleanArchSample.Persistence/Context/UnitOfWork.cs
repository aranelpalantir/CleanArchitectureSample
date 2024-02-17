using CleanArchSample.Application.Data;
using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Primitives;
using CleanArchSample.Persistence.Repositories;

namespace CleanArchSample.Persistence.Context
{
    internal sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public IGenericWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new() =>
            new GenericWriteRepository<T>(dbContext);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
