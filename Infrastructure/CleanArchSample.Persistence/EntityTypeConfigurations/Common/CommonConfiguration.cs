using CleanArchSample.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations.Common
{
    internal static class CommonConfiguration
    {
        public static void ConfigureCommonProperties<T>(this EntityTypeBuilder<T> builder) where T : BaseEntity
        {
            builder.Property(c => c.CreatedBy)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(c => c.LastModifiedBy)
                .HasMaxLength(256);
        }
    }
}
