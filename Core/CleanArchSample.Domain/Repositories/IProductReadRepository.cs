using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Domain.Repositories
{
    public interface IProductReadRepository : IReadRepository
    {
        Task<Product?> GetById(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
    }
}
