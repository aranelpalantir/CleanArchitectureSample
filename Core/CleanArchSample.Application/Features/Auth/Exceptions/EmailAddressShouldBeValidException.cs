using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class EmailAddressShouldBeValidException() : BaseRuleException("Böyle bir email adresi bulunmamaktadır.");
}
