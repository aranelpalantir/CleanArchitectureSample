using Bogus;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.EntityTypeConfigurations.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchSample.Persistence.EntityTypeConfigurations
{
    public class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            ConfigureProperties(builder);
            SeedData(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<Detail> builder)
        {
            builder.ConfigureCommonProperties();
            builder.Property(c => c.Title)
                .HasMaxLength(256)
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(256)
                .IsRequired();
        }

        private void SeedData(EntityTypeBuilder<Detail> builder)
        {
            Faker faker = new("tr");
            var id = 1;
            builder.HasData(new Detail
            {
                Id = id++,
                Title = faker.Lorem.Sentence(1),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Detail
            {
                Id = id++,
                Title = faker.Lorem.Sentence(2),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Detail
            {
                Id = id++,
                Title = faker.Lorem.Sentence(3),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            }, new Detail
            {
                Id = id++,
                Title = faker.Lorem.Sentence(2),
                Description = faker.Lorem.Sentence(5),
                CategoryId = 1,
                CreatedDate = DateTimeOffset.UtcNow,
                CreatedBy = "seed"
            });
        }
    }
}
