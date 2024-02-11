using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal class EmailAddressShouldBeValidException() : BaseRuleException("Böyle bir email adresi bulunmamaktadır.");
}
