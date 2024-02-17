using AutoMapper;
using CleanArchSample.Application.Data;
using CleanArchSample.Application.Features.Products.Commands.CreateProduct;
using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Entities;
using MediatR;
using Moq;

namespace CleanArchSample.Application.UnitTests.Features.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IMapper> _mapperMock = new();
        private readonly Mock<IPublisher> _publisherMock = new();
        private readonly Mock<IProductRule> _productRuleMock = new();

        private CreateProductCommandRequest SetupProductCommandRequest()
        {
            return new CreateProductCommandRequest
            {
                Title = "Title",
                Description = "Description",
                BrandId = 1,
                CategoryIds = [1],
                Discount = 1,
                Price = 1
            };
        }
        private CreateProductCommandHandler SetupProductCommandHandler()
        {
            return new CreateProductCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object, _publisherMock.Object, _productRuleMock.Object);
        }

        [Fact]
        public async Task Handle_Should_ThrowProductTitleMustNotBeSameException_WhenProductIsNotUnique()
        {
            //Arrange
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
            _mapperMock.Setup(m => m.Map<Product>(It.IsAny<CreateProductCommandRequest>())).Returns(new Product());

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
        public async Task Handle_Should_CallAddAsyncOnProductRepository_InIdealScenario()
        {
            //Arrange
            SetupMocksForInIdealScenario();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act
            await handler.Handle(command, default);

            //Assert
            _unitOfWorkMock.Verify(x =>
                x.GetWriteRepository<Product>().AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Once);
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
        public async Task Handle_Should_CallPublishProductCreatedDomainEventOnPublisher_InIdealScenario()
        {
            //Arrange
            SetupMocksForInIdealScenario();

            var command = SetupProductCommandRequest();

            var handler = SetupProductCommandHandler();

            //Act
            await handler.Handle(command, default);

            //Assert
            _publisherMock.Verify(x => x.Publish(It.IsAny<ProductCreatedDomainEvent>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
