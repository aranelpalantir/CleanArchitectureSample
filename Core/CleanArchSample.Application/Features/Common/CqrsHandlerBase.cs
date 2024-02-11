using System.Security.Claims;
using AutoMapper;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Common
{
    internal abstract class CqrsHandlerBase(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        protected readonly IUnitOfWork UnitOfWork = unitOfWork;
        protected readonly IMapper Mapper = mapper;
        protected readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;
        protected string? UserId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
