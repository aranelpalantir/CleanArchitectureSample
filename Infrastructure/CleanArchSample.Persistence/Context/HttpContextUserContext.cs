using CleanArchSample.Application.Abstractions.Security;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Persistence.Context
{
    internal sealed class HttpContextUserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public string? UserName => httpContextAccessor.HttpContext?.User?.Identity?.Name;
    }
}
