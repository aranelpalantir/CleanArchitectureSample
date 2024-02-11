using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal class EmailOrPasswordShouldNotBeInvalidException() : BaseRuleException("E-Posta adresi ya da parola hatalı!");
}
