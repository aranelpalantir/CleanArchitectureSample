using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandMapperProfile : Profile
    {
        public UpdateProductCommandMapperProfile()
        {
            CreateMap<UpdateProductCommandRequest, Product>();
        }
    }
}
