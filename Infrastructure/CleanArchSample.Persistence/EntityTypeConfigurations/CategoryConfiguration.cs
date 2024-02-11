using CleanArchSample.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            ConfigureProperties(builder);
            SeedData(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();
        }

        private void SeedData(EntityTypeBuilder<Category> builder)
        {
            var id = 1;
            builder.HasData(new Category
            {
                Id = id++,
                Name = "Elektronik",
                Priority = 1,
                ParentId = 0,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Category
            {
                Id = id++,
                Name = "Bilgisayar",
                Priority = 1,
                ParentId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Category
            {
                Id = id++,
                Name = "Moda",
                Priority = 2,
                ParentId = 0,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Category
            {
                Id = id++,
                Name = "Kadın",
                Priority = 1,
                ParentId = 3,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            });
        }
    }
}
