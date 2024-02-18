using System.Net;

namespace CleanArchSample.Application.Exceptions
{
    internal class BaseBusinessRuleException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; init; }
        public BaseBusinessRuleException() { }

        public BaseBusinessRuleException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) :
            base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
