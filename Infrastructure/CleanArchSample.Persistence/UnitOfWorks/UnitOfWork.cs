using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Common;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Repositories;

namespace CleanArchSample.Persistence.UnitOfWorks
{
    internal class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new() =>
            new ReadRepository<T>(dbContext);


        public IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new() =>
            new WriteRepository<T>(dbContext);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
