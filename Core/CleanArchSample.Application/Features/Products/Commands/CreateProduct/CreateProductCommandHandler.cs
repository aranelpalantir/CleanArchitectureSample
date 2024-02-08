using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : CqrsHandlerBase, IRequestHandler<CreateProductCommandRequest, Unit>
    {
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = Mapper.Map<Product>(request);
            product.CreatedBy = "-";
            product.CreatedDate = DateTimeOffset.UtcNow;

            await UnitOfWork.GetWriteRepository<Product>().AddAsync(product, cancellationToken);
            foreach (var categoryId in request.CategoryIds)
            {
                await UnitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory
                {
                    Product = product,
                    CategoryId = categoryId
                }, cancellationToken);
            }
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
