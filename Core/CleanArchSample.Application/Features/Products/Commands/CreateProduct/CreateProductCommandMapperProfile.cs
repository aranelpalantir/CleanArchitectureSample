using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    internal class CreateProductCommandMapperProfile : Profile
    {
        public CreateProductCommandMapperProfile()
        {
            CreateMap<CreateProductCommandRequest, Product>();
        }
    }
}
