using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Application.Data
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericWriteRepository<T> GetWriteRepository<T>() where T : class, IEntityBase, new();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
