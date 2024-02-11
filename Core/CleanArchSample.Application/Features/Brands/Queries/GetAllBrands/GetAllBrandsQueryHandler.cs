using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

internal class GetAllBrandsQueryHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor)
    : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor),
        IRequestHandler<GetAllBrandsQueryRequest, IReadOnlyList<GetAllBrandsQueryResponse>>
{
    public async Task<IReadOnlyList<GetAllBrandsQueryResponse>> Handle(GetAllBrandsQueryRequest request, CancellationToken cancellationToken)
    {
        var brands = await UnitOfWork.GetReadRepository<Brand>()
            .ToListAsync(cancellationToken: cancellationToken);

        var response = Mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<GetAllBrandsQueryResponse>>(brands);

        return response;
    }
}