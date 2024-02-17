using CleanArchSample.Application.Data;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                return await dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }

                throw;
            }

        }
    }
}
