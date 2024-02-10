using CleanArchSample.Application.Attributes;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    [RedisCache(5)]
    public class GetAllProductsQueryRequest : IRequest<IReadOnlyList<GetAllProductsQueryResponse>>
    {
    }
}
