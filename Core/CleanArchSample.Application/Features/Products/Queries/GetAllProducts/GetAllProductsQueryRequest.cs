using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryRequest : IRequest<IReadOnlyList<GetAllProductsQueryResponse>>
    {
    }
}
