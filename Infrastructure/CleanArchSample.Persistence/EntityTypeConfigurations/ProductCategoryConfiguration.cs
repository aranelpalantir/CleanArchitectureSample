using CleanArchSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations
{
    internal sealed class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(c => new { c.ProductId, c.CategoryId });

            builder.HasOne(c => c.Product)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(c => c.ProductId).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(c => c.CategoryId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
