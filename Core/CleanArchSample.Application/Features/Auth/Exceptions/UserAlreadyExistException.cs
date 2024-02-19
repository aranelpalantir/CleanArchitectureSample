using System.Net;
using CleanArchSample.Application.Enums;
using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class UserAlreadyExistException() : BaseBusinessRuleException("Böyle bir kullanıcı zaten var!", ErrorType.Conflict);
}
