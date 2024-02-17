using System.Linq.Expressions;
using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Primitives;
using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchSample.Persistence.Repositories
{
    internal sealed class GenericReadRepository<T>(AppDbContext dbContext) : IGenericReadRepository<T>
        where T : class, IEntityBase, new()
    {
        private DbSet<T> Table => dbContext.Set<T>();

        private static IQueryable<T> ApplyParameters(IQueryable<T> queryable, Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
        {
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include is not null)
                queryable = include(queryable);
            if (predicate is not null)
                queryable = queryable.Where(predicate);
            if (orderBy is not null)
                queryable = orderBy(queryable);
            return queryable;
        }

        public async Task<IReadOnlyList<T>> ToListAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, orderBy);
            return await queryable.ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<T>> ToListByPagingAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, int currentPage = 1,
            int pageSize = 3, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, orderBy);
            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, enableTracking: enableTracking);
            return await queryable.SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, enableTracking: enableTracking);
            return await queryable.SingleAsync(cancellationToken);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, enableTracking: enableTracking);
            return await queryable.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate, include, enableTracking: enableTracking);
            return await queryable.FirstAsync(cancellationToken);
        }

        public async Task<T?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await Table.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate);
            return await queryable.CountAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            var queryable = Table.AsQueryable();
            queryable = GenericReadRepository<T>.ApplyParameters(queryable, predicate);
            return await queryable.AnyAsync(cancellationToken);
        }
    }
}
