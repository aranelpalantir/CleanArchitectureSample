using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<bool> IsProductTitleExistAsync(string title, CancellationToken cancellationToken);
        Task<Product?> GetByIdWithProductCategories(int id, CancellationToken cancellationToken);
        Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken);
    }
}
