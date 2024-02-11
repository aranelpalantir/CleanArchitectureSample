using Bogus;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations
{
    internal class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            ConfigureProperties(builder);
            SeedData(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<Brand> builder)
        {
            builder.ConfigureCommonProperties();
            builder.Property(c => c.Name)
                .HasMaxLength(256)
                .IsRequired();
        }

        private void SeedData(EntityTypeBuilder<Brand> builder)
        {
            Faker faker = new("tr");
            var id = 1;
            builder.HasData(new Brand
            {
                Id = id++,
                Name = faker.Commerce.Department(),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Brand
            {
                Id = id++,
                Name = faker.Commerce.Department(),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Brand
            {
                Id = id++,
                Name = faker.Commerce.Department(),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            });
        }
    }
}
