using CleanArchSample.Application.Attributes;
using MediatR;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

[RequestCache]
public class GetAllBrandsQueryRequest : IRequest<IReadOnlyList<GetAllBrandsQueryResponse>>
{
}