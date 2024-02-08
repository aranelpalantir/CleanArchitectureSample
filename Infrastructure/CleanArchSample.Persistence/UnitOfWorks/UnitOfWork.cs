using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Common;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Repositories;

namespace CleanArchSample.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        public IReadRepository<T> GetReadRepository<T>() where T : class, IEntityBase, new() =>
            new ReadRepository<T>(_dbContext);


        public IWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new() =>
            new WriteRepository<T>(_dbContext);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
