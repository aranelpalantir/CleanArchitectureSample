using CleanArchSample.Application.Data;

namespace CleanArchSample.Persistence.Context
{
    internal sealed class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
    {
        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
