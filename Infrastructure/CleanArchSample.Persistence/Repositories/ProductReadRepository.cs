using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class ProductReadRepository(AppDbContext dbContext) : IProductReadRepository
    {
        public async Task<Product?> GetById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Include(r => r.ProductCategories)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        private IQueryable<Product> ProductsAsNoTracking => dbContext.Products.AsNoTracking();
        public async Task<IReadOnlyList<Product>> GetAll(CancellationToken cancellationToken)
        {
            return await ProductsAsNoTracking.Include(r => r.Brand).ToListAsync(cancellationToken);
        }
    }
}
