using CleanArchSample.Application.Attributes;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    [RequestCache(5)]
    public class GetAllProductsQueryRequest : IRequest<IReadOnlyList<GetAllProductsQueryResponse>>
    {
    }
}
