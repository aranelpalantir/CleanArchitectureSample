using System.Net;
using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class UserAlreadyExistException() : BaseRuleException("Böyle bir kullanıcı zaten var!", HttpStatusCode.Conflict);
}
