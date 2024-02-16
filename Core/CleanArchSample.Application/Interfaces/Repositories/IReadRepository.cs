using System.Linq.Expressions;
using CleanArchSample.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Query;

namespace CleanArchSample.Application.Interfaces.Repositories
{
    public interface IReadRepository<T> where T : class, IEntityBase, new()
    {
        Task<IReadOnlyList<T>> ToListAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, CancellationToken cancellationToken = default);

        Task<IReadOnlyList<T>> ToListByPagingAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int currentPage = 1,
            int pageSize = 3, CancellationToken cancellationToken = default);

        Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<T?> FindAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull;

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default);
    }
}
