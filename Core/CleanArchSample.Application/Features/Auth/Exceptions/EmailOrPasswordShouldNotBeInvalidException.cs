using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class EmailOrPasswordShouldNotBeInvalidException() : BaseRuleException("E-Posta adresi ya da parola hatalı!");
}
