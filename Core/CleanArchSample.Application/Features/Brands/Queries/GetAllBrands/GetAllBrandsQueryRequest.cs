using MediatR;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

public class GetAllBrandsQueryRequest : IRequest<IReadOnlyList<GetAllBrandsQueryResponse>>
{
}