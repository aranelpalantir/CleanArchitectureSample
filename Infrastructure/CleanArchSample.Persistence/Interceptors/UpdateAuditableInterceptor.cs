using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CleanArchSample.Domain.Primitives;
using CleanArchSample.Application.Interfaces.Security;

namespace CleanArchSample.Persistence.Interceptors
{
    public sealed class UpdateAuditableInterceptor(IUserContext userContext) : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                UpdateAuditableEntities(eventData.Context);
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditableEntities(DbContext context)
        {
            var utcNow = DateTimeOffset.UtcNow;
            var entities = context.ChangeTracker.Entries<EntityBase>().ToList();

            foreach (var entry in entities)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = GetUserName();
                    entry.Entity.CreatedDate = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = GetUserName();
                    entry.Entity.LastModifiedDate = utcNow;
                }
            }
        }

        private string GetUserName()
        {
            return userContext.UserName ?? "-";
        }
    }
}
