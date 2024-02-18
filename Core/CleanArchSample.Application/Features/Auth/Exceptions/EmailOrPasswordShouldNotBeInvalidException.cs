using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class EmailOrPasswordShouldNotBeInvalidException() : BaseBusinessRuleException("E-Posta adresi ya da parola hatalı!");
}
