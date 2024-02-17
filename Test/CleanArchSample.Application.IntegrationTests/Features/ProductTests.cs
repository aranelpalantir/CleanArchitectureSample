using CleanArchSample.Application.Features.Products.Commands.CreateProduct;
using CleanArchSample.Application.Features.Products.Commands.DeleteProduct;
using CleanArchSample.Application.Features.Products.Commands.UpdateProduct;
using CleanArchSample.Application.IntegrationTests.Abstractions;
using CleanArchSample.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.IntegrationTests.Features
{
    public class ProductTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
    {
        [Fact]
        public async Task Create_ShouldAddNewProductToDatabase()
        {
            //Arrange
            var command = new CreateProductCommandRequest
            {
                Title = Guid.NewGuid().ToString(),
                Description = "Test Description",
                BrandId = 1,
                Discount = 10,
                Price = 100,
                CategoryIds = new[] { 1, 2 }
            };

            //Act
            await Sender.Send(command);

            //Assert

            var product = DbContext.Products.SingleOrDefault(r => r.Title == command.Title);

            product.Should().NotBeNull();
            product.Description.Should().Be(command.Description);
            product.BrandId.Should().Be(command.BrandId);
            product.Discount.Should().Be(command.Discount);
            product.Price.Should().Be(command.Price);
            product.ProductCategories.Select(r => r.CategoryId).OrderBy(r => r).Should()
                .Equal(command.CategoryIds.Select(r => r).OrderBy(r => r));
            product.CreatedBy.Should().NotBeNull();
            product.CreatedDate.Should().BeBefore(DateTimeOffset.UtcNow);
            product.LastModifiedBy.Should().BeNull();
            product.LastModifiedDate.Should().BeNull();
        }
        [Fact]
        public async Task Update_ShouldUpdateProductToDatabase()
        {
            //Arrange
            var productEntity = new Product
            {
                Title = Guid.NewGuid().ToString(),
                Description = "Test Description",
                BrandId = 1,
                Discount = 10,
                Price = 100,
                ProductCategories = new List<ProductCategory>
                {
                    new() { CategoryId = 1 },
                    new() { CategoryId = 2 }
                }
            };
            DbContext.Products.Add(productEntity);
            await DbContext.SaveChangesAsync();

            var command = new UpdateProductCommandRequest
            {
                Id = productEntity.Id,
                Title = Guid.NewGuid().ToString(),
                Description = "Test Description-Updated",
                BrandId = 2,
                Discount = 20,
                Price = 1000,
                CategoryIds = new[] { 1 }
            };

            //Act
            await Sender.Send(command);

            //Assert

            var product = DbContext.Products.SingleOrDefault(r => r.Id == productEntity.Id);

            product.Should().NotBeNull();
            product.Description.Should().Be(command.Description);
            product.BrandId.Should().Be(command.BrandId);
            product.Discount.Should().Be(command.Discount);
            product.Price.Should().Be(command.Price);
            product.ProductCategories.Select(r => r.CategoryId).OrderBy(r => r).Should()
                .Equal(command.CategoryIds.Select(r => r).OrderBy(r => r));
            productEntity.CreatedBy.Should().Be(product.CreatedBy);
            productEntity.CreatedDate.Should().Be(product.CreatedDate);
            product.LastModifiedBy.Should().NotBeNull();
            product.LastModifiedDate.Should().BeBefore(DateTimeOffset.UtcNow);
        }

        [Fact]
        public async Task Delete_ShouldDeleteProductFromDatabase()
        {
            //Arrange
            var productEntity = new Product
            {
                Title = Guid.NewGuid().ToString(),
                Description = "Test Description",
                BrandId = 1,
                Discount = 10,
                Price = 100,
                ProductCategories = new List<ProductCategory>
                {
                    new() { CategoryId = 1 },
                    new() { CategoryId = 2 }
                }
            };
            DbContext.Products.Add(productEntity);
            await DbContext.SaveChangesAsync();

            var command = new DeleteProductCommandRequest
            {
                Id = productEntity.Id
            };

            //Act
            await Sender.Send(command);

            //Assert

            var product = DbContext.Products.SingleOrDefault(r => r.Id == productEntity.Id);
            var productCategories = await DbContext.ProductCategories.Where(r => r.ProductId == productEntity.Id).ToListAsync();

            Assert.Null(product);
            productCategories.Count.Should().Be(0);
        }
    }
}
