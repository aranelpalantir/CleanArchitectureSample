﻿using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandMapperProfile : Profile
    {
        public UpdateProductCommandMapperProfile()
        {
            CreateMap<UpdateProductCommandRequest, Product>();
        }
    }
}