using System.Net;

namespace CleanArchSample.Application.Exceptions
{
    internal class BaseRuleException : ApplicationException
    {
        public HttpStatusCode StatusCode { get; init; }
        public BaseRuleException() { }

        public BaseRuleException(string message, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) :
            base(message)
        {
            StatusCode = httpStatusCode;
        }
    }
}
