using AutoMapper;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

internal sealed class GetAllBrandsQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper) :
    IRequestHandler<GetAllBrandsQueryRequest, IReadOnlyList<GetAllBrandsQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
    {
        var brands = await unitOfWork.GetReadRepository<Brand>()
            .ToListAsync(cancellationToken: cancellationToken);

        var response = mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<GetAllBrandsQueryResponse>>(brands);

        return response;
    }
}