using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    public class EmailOrPasswordShouldNotBeInvalidException : BaseRuleException
    {
        public EmailOrPasswordShouldNotBeInvalidException() : base("E-Posta adresi ya da parola hatalı!")
        {

        }
    }
}
