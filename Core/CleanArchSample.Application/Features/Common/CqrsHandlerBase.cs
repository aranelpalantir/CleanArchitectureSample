using System.Security.Claims;
using AutoMapper;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Common
{
    public abstract class CqrsHandlerBase
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected string? UserId;

        protected CqrsHandlerBase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            HttpContextAccessor = httpContextAccessor;
            UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
