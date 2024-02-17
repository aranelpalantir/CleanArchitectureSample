using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class BrandRepository(AppDbContext dbContext) : BaseRepository<Brand>(dbContext), IBrandRepository
    {
        public async Task<Product?> GetById(int id, CancellationToken cancellationToken)
        {
            return await DbContext.Products
                .Include(r => r.ProductCategories)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        private IQueryable<Brand> BrandsAsNoTracking => DbContext.Brands.AsNoTracking();
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
