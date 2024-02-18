using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class EmailAddressShouldBeValidException() : BaseBusinessRuleException("Böyle bir email adresi bulunmamaktadır.");
}
