using CleanArchSample.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations.Common
{
    internal static class CommonConfiguration
    {
        public static void ConfigureCommonProperties<T>(this EntityTypeBuilder<T> builder) where T : EntityBase
        {
            builder.Property(c => c.CreatedBy)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(c => c.LastModifiedBy)
                .HasMaxLength(256);
        }
    }
}
