using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Domain.Repositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<Product?> GetById(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Brand>> GetAll(CancellationToken cancellationToken);
        Task<int> CountAsync(CancellationToken cancellationToken = default);
    }
}
