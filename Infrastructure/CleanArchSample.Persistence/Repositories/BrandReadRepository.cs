using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class BrandReadRepository(AppDbContext dbContext) : IBrandReadRepository
    {
        public async Task<Product?> GetById(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Include(r => r.ProductCategories)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        private IQueryable<Brand> BrandsAsNoTracking => dbContext.Brands.AsNoTracking();
        public async Task<IReadOnlyList<Brand>> GetAll(CancellationToken cancellationToken)
        {
            return await BrandsAsNoTracking.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            return await BrandsAsNoTracking.CountAsync(cancellationToken);
        }
    }
}
