using AutoMapper;
using CleanArchSample.Application.Features.Products.Queries.GetAllProducts.Dtos;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Queries.GetAllProducts
{
    internal class GetAllProductsQueryMapperProfile : Profile
    {
        public GetAllProductsQueryMapperProfile()
        {
            CreateMap<Product, GetAllProductsQueryResponse>();
            CreateMap<Brand, BrandDto>();
        }
    }
}
