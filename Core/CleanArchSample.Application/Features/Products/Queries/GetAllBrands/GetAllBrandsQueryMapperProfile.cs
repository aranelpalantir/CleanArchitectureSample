using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllBrands
{
    public class GetAllBrandsQueryMapperProfile : Profile
    {
        public GetAllBrandsQueryMapperProfile()
        {
            CreateMap<Brand, GetAllBrandsQueryResponse>();
        }
    }
}
