using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class ProductRepository(AppDbContext dbContext) : BaseRepository<Product>(dbContext), IProductRepository
    {
        public async Task<bool> IsProductTitleExistAsync(string title, CancellationToken cancellationToken)
        {
            return await DbContext.Products.AnyAsync(r => r.Title == title, cancellationToken);
        }

        public async Task<Product?> GetByIdWithProductCategories(int id, CancellationToken cancellationToken)
        {
            return await DbContext.Products
                .Include(r => r.ProductCategories)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        private IQueryable<Product> ProductsAsNoTracking => DbContext.Products.AsNoTracking();
        public async Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken)
        {
            return await ProductsAsNoTracking.Include(r => r.Brand).ToListAsync(cancellationToken);
        }
    }
}
