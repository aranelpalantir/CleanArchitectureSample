using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Brands.Queries.GetAllBrands;

internal class GetAllBrandsQueryMapperProfile : Profile
{
    public GetAllBrandsQueryMapperProfile()
    {
        CreateMap<Brand, GetAllBrandsQueryResponse>();
    }
}