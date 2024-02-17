using AutoMapper;
using CleanArchSample.Application.Data;
using CleanArchSample.Application.Features.Products.Commands.UpdateProduct;
using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;
using Moq;

namespace CleanArchSample.Application.UnitTests.Features.Products.Commands
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IProductReadRepository> _productReadRepositoryMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IPublisher> _publisherMock = new();
        private readonly Mock<IProductRule> _productRuleMock = new();

        private UpdateProductCommandRequest SetupProductCommandRequest()
        {
            return new UpdateProductCommandRequest
            {
                Title = "Title",
                Description = "Description",
                BrandId = 1,
                CategoryIds = [1],
                Discount = 1,
                Price = 1
            };
        }
        private UpdateProductCommandHandler SetupProductCommandHandler()
        {
            return new UpdateProductCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _publisherMock.Object, _productReadRepositoryMock.Object, _productRuleMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ThrowProductNotFoundException_WhenProductIsNotFound()
        {
            //Arrange

            _productReadRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<Product?>(null));

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act And Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(async () =>
                await handler.Handle(command, default));

        }

        [Fact]
        public async Task Handle_Should_ThrowProductTitleMustNotBeSameException_WhenProductIsNotUnique()
        {
            //Arrange

            _productReadRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Product())!);

            _productRuleMock.Setup(x =>
                    x.ProductTitleMustNotBeSame(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<ProductTitleMustNotBeSameException>();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act And Assert
            await Assert.ThrowsAsync<ProductTitleMustNotBeSameException>(async () =>
                await handler.Handle(command, default));

        }
        private void SetupMocksForInIdealScenario()
        {
            _productReadRepositoryMock
                .Setup(x => x.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new Product())!);

            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<UpdateProductCommandRequest>())).Returns(new Product());

            _unitOfWorkMock
                .Setup(x => x.GetWriteRepository<Product>()
                    .AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        }
        [Fact]
        public async Task Handle_Should_ReturnUnitValue_InIdealScenario()
        {
            //Arrange
            SetupMocksForInIdealScenario();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Equal(Unit.Value, result);

        }

        [Fact]
        public async Task Handle_Should_CallSaveChangesAsyncOnUnitOfWork_InIdealScenario()
        {
            //Arrange
            SetupMocksForInIdealScenario();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act
            await handler.Handle(command, default);

            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task Handle_Should_CallPublishProductUpdatedDomainEventOnPublisher_InIdealScenario()
        {
            //Arrange
            SetupMocksForInIdealScenario();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act
            await handler.Handle(command, default);

            //Assert
            _publisherMock.Verify(x => x.Publish(It.IsAny<ProductUpdatedDomainEvent>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
