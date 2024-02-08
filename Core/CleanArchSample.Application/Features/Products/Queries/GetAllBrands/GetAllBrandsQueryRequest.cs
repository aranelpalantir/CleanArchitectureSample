using MediatR;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllBrands
{
    public class GetAllBrandsQueryRequest : IRequest<IReadOnlyList<GetAllBrandsQueryResponse>>
    {
    }
}
