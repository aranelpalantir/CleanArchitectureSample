using Bogus;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            ConfigureProperties(builder);
            SeedData(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<Product> builder)
        {
            builder.ConfigureCommonProperties();
            builder.Property(c => c.Title)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(c => c.Discount).HasPrecision(18, 2);
            builder.Property(c => c.Price).HasPrecision(18, 2);
        }

        private void SeedData(EntityTypeBuilder<Product> builder)
        {
            Faker faker = new("tr");
            var id = 1;
            builder.HasData(new Product
            {
                Id = id++,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 1,
                Discount = faker.Random.Decimal(0, 10),
                Price = faker.Finance.Amount(10, 1000),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Product
            {
                Id = id++,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 2,
                Discount = faker.Random.Decimal(0, 10),
                Price = faker.Finance.Amount(10, 1000),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Product
            {
                Id = id++,
                Title = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription(),
                BrandId = 3,
                Discount = faker.Random.Decimal(0, 10),
                Price = faker.Finance.Amount(10, 1000),
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            });
        }
    }
}
