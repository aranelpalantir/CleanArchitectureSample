using AutoMapper;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

internal sealed class GetAllBrandsQueryHandler(
    IBrandReadRepository brandReadRepository,
    IMapper mapper) :
    IRequestHandler<GetAllBrandsQueryRequest, IReadOnlyList<GetAllBrandsQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
    {
        var brands = await brandReadRepository.GetAll(cancellationToken: cancellationToken);

        var response = mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<GetAllBrandsQueryResponse>>(brands);

        return response;
    }
}