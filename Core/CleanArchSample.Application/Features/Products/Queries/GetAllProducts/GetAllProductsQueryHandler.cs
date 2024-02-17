using AutoMapper;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    internal sealed class GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper) :
        IRequestHandler<GetAllProductsQueryRequest, IReadOnlyList<GetAllProductsQueryResponse>>
    {
        public async Task<IReadOnlyList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAll(cancellationToken);

            var response = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<GetAllProductsQueryResponse>>(products);

            return response;
        }
    }
}
