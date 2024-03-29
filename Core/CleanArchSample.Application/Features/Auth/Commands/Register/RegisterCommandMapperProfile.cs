﻿using AutoMapper;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Auth.Commands.Register
{
    internal sealed class RegisterCommandMapperProfile : Profile
    {
        public RegisterCommandMapperProfile()
        {
            CreateMap<RegisterCommandRequest, User>();
        }
    }
}
